using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;

#line hidden
namespace _.__._0x0005
{
    [DebuggerStepThrough]
    [DebuggerNonUserCode]
    public static class SE
    {
        [DebuggerStepThrough]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.Synchronized)]
        public static byte[] ToByteArray(this Stream stream)
        {
            using (stream)
            using (var memStream = new MemoryStream())
            {
                stream.CopyTo(memStream);
                return memStream.ToArray();
            }
        }
    }
}
#line default