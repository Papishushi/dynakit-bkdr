using ProtoBuf.Meta;
using _.__._0x0005;
using System.Diagnostics;
using System.Runtime.CompilerServices;

#line hidden
namespace _.__._0x0006
{
    [DebuggerStepThrough]
    [DebuggerNonUserCode]
    public sealed class SCD : SCSS
    {
        [DebuggerStepThrough]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.Synchronized)]
        public SCD(RuntimeTypeModel m, int gBS, string bN, string cBN) :
            base(bN, cBN, m, gBS)
        { }

        [DebuggerStepThrough]
        [DebuggerNonUserCode]
        public readonly record struct DO(Type Type, SE Error, DeeplyMutableType Output, string Path, bool Decompressed);
        [DebuggerStepThrough]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.Synchronized)]
        private DO DFP (Type t, string p, bool d, object? o)
        {
            DO @do;
            object? ds = default;


            using (var r = File.OpenRead(Path.Combine(p, GFN(t, d))))
            {
                byte[] dc = r.ToByteArray();

                if (d)
                    dc = DC(dc);

                using (var ms = new MemoryStream(dc, 0, dc.Length, false))
                {
                    ds = _m!.DeserializeWithLengthPrefix(ms, o, t, ProtoBuf.PrefixStyle.Base128, 0);
                }
            }

            @do = new() { Error = SE.None, Output = new(ds), Type = t, Path = p, Decompressed = d };
            return @do;
        }
        [DebuggerStepThrough]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.Synchronized)]
        private DO DFM(Type t, byte[] s, bool d, object? o)
        {
            DO @do;
            object? ds = default;
            byte[] dc = s;

            if (d)
                dc = DC(s);

            using (var ms = new MemoryStream(dc, 0, dc.Length, false))
            {
                ds = _m!.DeserializeWithLengthPrefix(ms, o, t, ProtoBuf.PrefixStyle.Base128, 0);
                //Console.WriteLine(deserialized);
            }

            @do = new() { Error = SE.None, Output = new(ds), Type = t, Path = string.Empty, Decompressed = d };
            return @do;
        }
        [DebuggerStepThrough]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.Synchronized)]
        public DO DS<T>(string p, T? o = default, bool d = false)
        {
            if (CFSE(typeof(T), out var output)) return new() { Error = output!.Value, Output = new(), Type = typeof(T), Path = p, Decompressed = d };
            if (string.IsNullOrEmpty(p)) return new() { Error = SE.NullPath, Output = new(), Type = typeof(T), Path = p, Decompressed = d };
            try
            {
                return DFP(typeof(T), p, d, o);
            }
            catch (Exception ex)
            {
                return new() { Error = HSE(ex), Output = new(), Type = typeof(T), Path = p, Decompressed = d };
            }
        }
        [DebuggerStepThrough]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.Synchronized)]
        public DO DS(Type t, string p, object? o = default, bool d = false)
        {
            if (CFSE(t, out var output)) return new() { Error =output!.Value, Output = new(), Type = t, Path = p, Decompressed = d };
            if (string.IsNullOrEmpty(p)) return new() { Error = SE.NullPath, Output = new(), Type = t, Path = p, Decompressed = d };
            try
            {
                return DFP(t, p, d, o);
            }
            catch (Exception ex)
            {
                return new() { Error = HSE(ex), Output = new(), Type = t, Path = p, Decompressed = d };
            }
        }
        [DebuggerStepThrough]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.Synchronized)]
        public DO DS<T>(byte[] s, T? o = default, bool d = false)
        {
            if (CFSE(typeof(T), out var output)) return new() { Error = output!.Value, Output = new(), Type = typeof(T), Path = string.Empty, Decompressed = d };
            try
            {
                return DFM(typeof(T), s, d, o);
            }
            catch (Exception ex)
            {
                return new() { Error = HSE(ex), Output = new(), Type = typeof(T), Path = string.Empty, Decompressed = d };
            }
        }
        [DebuggerStepThrough]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.Synchronized)]
        public DO DS(Type t, byte[] s, object? o = default, bool d = false)
        {
            if (CFSE(t, out var output)) return new() { Error = output!.Value, Output = new(), Type = t, Path = string.Empty, Decompressed = d };
            try
            {
                return DFM(t, s, d, o);
            }
            catch (Exception ex)
            {
                return new() { Error = HSE(ex), Output = new(), Type = t, Path = string.Empty, Decompressed = d };
            }
        }
    }
}
#line default