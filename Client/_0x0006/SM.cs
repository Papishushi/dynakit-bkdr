using ProtoBuf.Meta;
using _.__._0x0005;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using static _.__._0x0006.RMF;

#line hidden
namespace _.__._0x0006
{
    [DebuggerStepThrough]
    [DebuggerNonUserCode]
    public static class SM
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private const string P_C_B = ".pb.bin.gz";
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private const string P_B = ".pb.bin";

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static int _bI = 1024;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static int _gBS = (Environment.Is64BitOperatingSystem ? 64 : 32) * _bI;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static RMF? _mF = null;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static SCD? _ds = null;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly static Type[] _bIT;
        [DebuggerHidden]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal static TypeModel? M { get => _mF?.M; }
        [DebuggerHidden]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public static SCD? DS { get => _ds; }
        [DebuggerHidden]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public static int CBS { get => _gBS; }
        [DebuggerHidden]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public static int CBSI { get => _bI; set => _bI = value; }

        [DebuggerStepThrough]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.Synchronized)]
        static SM()
        {
            var a = typeof(SM).Assembly;
            var p = Path.Combine(a.Location[..a.Location.LastIndexOf('\\')], @"BuildinTypes.xml") ??
                throw new FileNotFoundException();

            var tN = PXTTA(p);
            if (tN.Length == 0)
                throw new InvalidDataException();

            var l = new List<Type>();
            var aT = a.GetTypes();

            foreach (var t in aT)
                if (tN.Contains(t.Name))
                    l.Add(t);

            _bIT = [.. l];
            _mF = new(_bIT);
            CS(_mF.M!);
        }

        [DebuggerStepThrough]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.Synchronized)]
        private static string[] PXTTA(string xFP)
        {
            var xD = XDocument.Load(xFP);
            XNamespace ns = "http://schemas.microsoft.com/powershell/2004/04";
            if (xD == null || xD!.Root == null) return [];
            var ts = xD.Root.Elements(ns + "Type").Select(t => t.Value).ToArray();
            return ts;
        }

        [DebuggerStepThrough]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.Synchronized)]
        private static void CS(RuntimeTypeModel rM)
        {
            _ds = new(rM, _gBS, P_B, P_C_B);
        }

        [DebuggerStepThrough]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.Synchronized)]
        public static void AT(Type t) => _mF?.AT(t);

        [DebuggerStepThrough]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.Synchronized)]
        public static CMO CM(RuntimeTypeModel m)
        {
            var o = (_mF ??= new(_bIT)).ChangeModel(m);
            CS(m);
            return o;
        }

        [DebuggerStepThrough]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.Synchronized)]
        public static void RF() => _mF = new RMF(_bIT);

    }
}
#line default