using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Xml.Linq;

//This is an example payload
public class LibClass
{
    byte[] sessionKey = GenerateRandomKey();
    byte[] sessionIV = GenerateRandomIV();
    XElement os = new("OS", System.Runtime.InteropServices.RuntimeInformation.OSDescription);
    XElement machineName = new("MachineName", Environment.MachineName);
    XElement processes = new XElement("ProcessList");
    string? localIP;

    public void LibMethod()
    {
        using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
        {
            socket.Connect("8.8.8.8", 65530);
            IPEndPoint? endPoint = socket.LocalEndPoint as IPEndPoint;
            localIP = endPoint?.Address.ToString();
        }
        if (string.IsNullOrEmpty(localIP)) localIP = "IP not found";
        XElement ip = new("IP", localIP);
        foreach (var process in Process.GetProcesses().AsEnumerable())
        {
            var xElement = new XElement("Process", process.ProcessName);
            xElement.Add(new XElement("PID", process.Id));
            var threads = string.Join(',', process.Threads.Cast<ProcessThread>().Select(t => t.Id));
            xElement.Add(new XElement("Threads", threads));
            processes.Add(xElement);
        }
        XElement xFile = new($"log-{DateTime.Now:ddMMyyyy-hhmmss}-{machineName.Value}", os, machineName, ip, processes);
        var a = Encrypt(xFile.ToString(), sessionKey, sessionIV);
        var b = Decrypt(a, sessionKey, sessionIV);
        Console.WriteLine(b);
    }

    private static byte[] GenerateRandomKey() { using (Aes aes = Aes.Create()) { aes.GenerateKey(); return aes.Key; } }
    private static byte[] GenerateRandomIV() { using (Aes aes = Aes.Create()) { aes.GenerateIV(); return aes.IV; } }
    private static string Encrypt(string text, byte[] key, byte[] iv) { using (var aesAlg = Aes.Create()) { aesAlg.Key = key; aesAlg.IV = iv; var encryptor = aesAlg.CreateEncryptor(); using (MemoryStream msEncrypt = new MemoryStream()) { using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write)) using (var swEncrypt = new StreamWriter(csEncrypt)) swEncrypt.Write(text); return Convert.ToBase64String(msEncrypt.ToArray()); } } }
    private static string Decrypt(string cipherText, byte[] key, byte[] iv) { using (var aesAlg = Aes.Create()) { aesAlg.Key = key; aesAlg.IV = iv; var decryptor = aesAlg.CreateDecryptor(); using (var msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText))) using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read)) using (var srDecrypt = new StreamReader(csDecrypt)) return srDecrypt.ReadToEnd(); } }
}