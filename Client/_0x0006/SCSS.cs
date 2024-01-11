using ProtoBuf.Meta;
using System.Diagnostics;
using System.Runtime.CompilerServices;

#line hidden
namespace _.__._0x0006
{
    [DebuggerStepThrough]
    [DebuggerNonUserCode]
    public abstract class SCSS : GZS
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        protected readonly string _bN;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        protected readonly string _cBN;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        protected readonly RuntimeTypeModel _m;

        public enum SE
        {
            None,
            NotSerializable,
            UnauthorizedAccess,
            PathNotValid,
            NullPath,
            PathTooLong,
            DirectoryNotFound,
            FileNotFound,
            NotSupported,
            IO,
            Serilog,
            ModelNull
        }
        [DebuggerStepThrough]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.Synchronized)]
        protected SCSS(string bN, string cBN, RuntimeTypeModel m, int s) : base(s)
        {
            _bN = bN;
            _cBN = cBN;
            _m = m;
        }
        [DebuggerStepThrough]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.Synchronized)]
        protected string GFN(Type t, bool c) => c ? t.Name + _cBN : t.Name + _bN;
        [DebuggerStepThrough]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.Synchronized)]
        protected bool CFSE(Type t, out SE? r)
        {
            if (_m == null)
            {
                r = SE.ModelNull;
                return true;
            }
            if (!_m!.CanSerialize(t))
            {
                r = SE.NotSerializable;
                return true;
            }
            r = null;
            return false;
        }
        [DebuggerStepThrough]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.Synchronized)]
        protected static SE HSE(Exception ex)
        {
            SE e = ex switch
            {
                UnauthorizedAccessException => SE.UnauthorizedAccess,
                ArgumentNullException => SE.NullPath,
                ArgumentException => SE.PathNotValid,
                PathTooLongException => SE.PathTooLong,
                DirectoryNotFoundException => SE.DirectoryNotFound,
                FileNotFoundException => SE.FileNotFound,
                NotSupportedException => SE.NotSupported,
                IOException => SE.IO,
                _ => SE.Serilog,
            };
            return e;
        }
    }
}
#line default