using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

#line hidden
namespace _.__._0x0004
{
    internal partial class AliceRabbitHole
    {
        [DebuggerStepThrough]
        [DebuggerNonUserCode]
        private class SelfLIResolver : ILIResolver
        {
            [DebuggerStepThrough]
            [DebuggerHidden]
            [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.Synchronized)]
            public string ResolveLI()
            {
                string? li;
                do
                {
                    using (var s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
                    {
                        s.Connect("8.8.8.8", 65530);
                        IPEndPoint? ep = s.LocalEndPoint as IPEndPoint;
                        li = ep?.Address.ToString();
                    }
                } while (li == null);
                return li;
            }
        }
    }
}
#line default