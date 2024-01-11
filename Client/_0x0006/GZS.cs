using System.Diagnostics;
using System.IO.Compression;
using System.Runtime.CompilerServices;

#line hidden
namespace _.__._0x0006
{
    [DebuggerStepThrough]
    [DebuggerNonUserCode]
    public abstract class GZS
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        protected readonly int _s;

        [DebuggerStepThrough]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.Synchronized)]
        protected GZS(int s) => _s = s;

        [DebuggerStepThrough]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.Synchronized)]
        public virtual byte[] C(byte[] d)
        {
            using (var cS = new MemoryStream())
            {
                using (var zS = new GZipStream(cS, CompressionMode.Compress))
                {
                    using (var bs = new BufferedStream(zS, _s))
                    {
                        bs.Write(d, 0, d.Length);
                        bs.Close();
                        zS.Close();
                        return cS.ToArray();
                    }
                }
            }
        }

        [DebuggerStepThrough]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.Synchronized)]
        public virtual byte[] DC(byte[] d)
        {
            using (var cS = new MemoryStream(d))
            {
                using (var zS = new GZipStream(cS, CompressionMode.Decompress))
                {
                    using (var bs = new BufferedStream(zS, _s))
                    {
                        using (var rS = new MemoryStream())
                        {
                            bs.CopyTo(rS);
                            bs.Close();
                            zS.Close();
                            return rS.ToArray();
                        }
                    }
                }
            }
        }
    }
}
#line default