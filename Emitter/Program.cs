using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Reflection;

IPAddress? _localAddr;
Console.WriteLine("Starting Server");
do
{
    using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
    {
        socket.Connect("8.8.8.8", 65530);
        IPEndPoint? endPoint = socket.LocalEndPoint as IPEndPoint;
        _localAddr = endPoint?.Address;
    }
} while (_localAddr == null);
var path = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName + @"\Payload.cs";
await new Server(path).StartServer(_localAddr, 43215);

#pragma warning disable CS8321 // Local function is declared but never used
static string GetHexaString(string input)
{
    StringBuilder sb = new StringBuilder();
    foreach (var c in input)
    {
        sb.Append(string.Format(@"\x{0:x4}", (ushort)c));
    }
    return sb.ToString();
}
#pragma warning restore CS8321 // Local function is declared but never used

#line default