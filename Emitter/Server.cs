
using _.__._0x0005;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

public class Server
{
    // Set the TcpListener on port 12345.
    public string path = string.Empty;
    string Instructions { get => File.ReadAllText(path); }
    private readonly List<TcpClient> _clients = new();
    private readonly List<Task> _activeClients = new();

    private readonly static Type[] _managers =
    {
        typeof(SerializationManager)
    };

    public Server(string path)
    {
        Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.Async(wt => wt.Console(theme: AnsiConsoleTheme.Code)).CreateLogger();
        try
        {
            foreach (var manager in _managers)
            {
                Log.Debug("Initializing {ty} ...", manager);
                RuntimeHelpers.RunClassConstructor(manager.TypeHandle);
            }

        }
        catch (Exception ex)
        {
            Log.Error("Manager constructor errror:{ex}\n{exin}", ex.Message, ex.InnerException?.Message);
            throw;
        }
        this.path = path;
    }

    ~Server()
    {
        Log.CloseAndFlush();
    }

    internal async Task StartServer(IPAddress iPAddress, int port)
    {
        TcpListener? server = null;

        try
        {
            server = new TcpListener(iPAddress, port);

            // Start listening for client requests.
            server.Start();

            Log.Information("Server started. Waiting for connections...");

            // Enter the listening loop.
            await Task.Run(() =>
            {
                while (true)
                {
                    TcpClient client = server.AcceptTcpClient();
                    Log.Information($"Client connected: {(client.Client.RemoteEndPoint as IPEndPoint)?.Address}");

                    _clients.Add(client);
                    // Handle the client in a separate thread or method.
                    _activeClients.Add(Task.Run(() => HandleClient(client)));
                }
            });
        }
        catch (Exception e)
        {
            Log.Error(e.ToString());
        }
        finally
        {
            // Stop listening for new clients.
            server?.Stop();
        }
    }

    private void HandleClient(TcpClient client)
    {
        var guid = Guid.NewGuid();
        var lastInstructions = string.Empty;
        var key = GenerateRandomKey();
        var iv = GenerateRandomIV();
        try
        {
            bool first = true;
            bool second = false;
            var stream = client.GetStream();

            while (true)
            {
                if (first || second)
                {
                    if (second)
                    {
                        Log.Verbose("{a}", iv);
                        stream.Write(iv, 0, iv.Length);
                        second = false;
                    }
                    if (first)
                    {
                        Log.Verbose("{a}", key);
                        stream.Write(key, 0, key.Length);
                        first = false;
                        second = true;
                    }
                }
                else
                {
                    stream.WriteByte(0);
                    Thread.Sleep(50);
                    if (!string.IsNullOrEmpty(Instructions) && lastInstructions != Instructions)
                    {
                        var output = SerializationManager.Serializer?.Serialize(Encrypt(Instructions, key, iv), true) ?? default;
                        if (output.Error != _.__._0x0005.Streamers.ScapeCoreSeralizationStreamer.SerializationError.None)
                            Log.Error("{c}", output.Error);
                        stream.Write(output.Data ?? [0], 0, (int)output.Size);
                        Log.Information("Updated instructions sent to client {guid} from {client} | {size} bytes sent", guid, client.Client.LocalEndPoint?.ToString(), output.Size);
                        lastInstructions = Instructions;
                    }
                }
                Thread.Sleep(1000);
            }
        }
        catch (IOException ex)
        {
            var _ = ex;
            Log.Warning($"Client disconnected remotely: {(client.Client.RemoteEndPoint as IPEndPoint)?.Address}");
        }
        catch (Exception e)
        {
            Log.Error(e.Message);
        }
        finally
        {
            client.Close();
        }
    }

    private static byte[] GenerateRandomKey() { using (Aes aes = Aes.Create()) { aes.GenerateKey(); return aes.Key; } }

    private static byte[] GenerateRandomIV() { using (Aes aes = Aes.Create()) { aes.GenerateIV(); return aes.IV; } }

    private static string Encrypt(string text, byte[] key, byte[] iv) { using (var aesAlg = Aes.Create()) { aesAlg.Key = key; aesAlg.IV = iv; var encryptor = aesAlg.CreateEncryptor(); using (MemoryStream msEncrypt = new MemoryStream()) { using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write)) using (var swEncrypt = new StreamWriter(csEncrypt)) swEncrypt.Write(text); return Convert.ToBase64String(msEncrypt.ToArray()); } } }

    private static string Decrypt(string cipherText, byte[] key, byte[] iv) { using (var aesAlg = Aes.Create()) { aesAlg.Key = key; aesAlg.IV = iv; var decryptor = aesAlg.CreateDecryptor(); using (var msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText))) using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read)) using (var srDecrypt = new StreamReader(csDecrypt)) return srDecrypt.ReadToEnd(); } }
}