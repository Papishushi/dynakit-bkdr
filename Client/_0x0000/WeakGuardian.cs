using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

#line hidden
namespace _.__._0x0000
{
    [DebuggerStepThrough]
    [DebuggerNonUserCode]
    internal class WeakGuardian
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        dynamic k = (byte)0;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        dynamic? g = null;

        [DebuggerStepThrough]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.NoInlining)]
        internal WeakGuardian(Action a)
        {
            try
            {
                Task.Run(async () =>
                {
                    var i = 1;
                    while (i == 1)
                    {
#if DEBUG
                        await Task.Delay(Random.Shared.Next(10000,20000));
                        throw new Exception();
#else
                        await Task.Delay(Random.Shared.Next(1000, 2000));
                        if (Debugger.IsAttached)
                            throw new Exception();
                        a();
#endif
                    }
                }).Wait();
            }
            catch
            {
                [DebuggerStepThrough]
                [DebuggerHidden]
                [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.Synchronized)]
                static void Gehena()
                {
                    while (Debugger.IsAttached)
                        Debugger.Break();
                }
                g = Task.Run(Gehena);
                k = 1;
            }
            if (k == 1)
            {
                while (g?.Status == TaskStatus.Running)
                    Thread.Sleep(Random.Shared.Next(10000, 20000));
                new WeakGuardian(a);
            }
        }
    }
}
#line default