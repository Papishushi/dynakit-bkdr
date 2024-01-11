using ProtoBuf.Meta;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using _.__._0x0005;
#line hidden
namespace _.__._0x0006
{
    [DebuggerStepThrough]
    [DebuggerNonUserCode]
    public sealed class RMF
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private const string N = "DynamicLibrary";
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private const int F_P_I = 1;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private const int S_P_I = 556;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private RuntimeTypeModel? _m = null;
        [DebuggerHidden]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public RuntimeTypeModel? M { get => _m; }

        [DebuggerStepThrough]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.Synchronized)]
        public RMF(Type[] ts)
        {
            var rM = CRM();
            foreach (var t in ts)
                CT(rM, t);
            rM.MakeDefault();
            _m = rM;
        }

        [DebuggerStepThrough]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.Synchronized)]
        public void AT(Type t)
        {
            if (_m == null)
            {
                return;
            }
            var fI = F_P_I;
            var mT = _m.Add(t, false);
            mT.IgnoreUnknownSubTypes = false;
            STF(mT, t, ref fI);
            SST(t, _m);
            if (t == typeof(DeeplyMutableType) || t.BaseType == typeof(DeeplyMutableType)) return;
            STP(mT, t, ref fI);
        }

        [DebuggerStepThrough]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.Synchronized)]
        private RuntimeTypeModel CRM()
        {
            var rM = RuntimeTypeModel.Create(N);
            rM.AllowParseableTypes = true;
            rM.AutoAddMissingTypes = true;
            rM.MaxDepth = 100;
            return rM;
        }

        [DebuggerStepThrough]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.Synchronized)]
        private void CT(RuntimeTypeModel rM, Type t)
        {
            var fI = F_P_I;
            var mT = rM.Add(t, false);
            if (t.IsEnum) return;
            SFSTAP(rM, t, mT, ref fI);
        }

        [DebuggerStepThrough]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.Synchronized)]
        private void SFSTAP(RuntimeTypeModel rM, Type t, MetaType mT, ref int fI)
        {
            STF(mT, t, ref fI);
            SST(t, rM);
            if (CFDMT(rM, t)) return;
            STP(mT, t, ref fI);
        }

        [DebuggerStepThrough]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.Synchronized)]
        private void STF(MetaType mT, Type t, ref int fI)
        {
            foreach (var f in t.GetFields())
            {
                try
                {
                    if (f.FieldType.Name == typeof(object).Name)
                    {
                        continue;
                    }
                    AF(mT, f, t, ref fI);
                }
                catch
                {
                    continue;
                }
            }
        }

        [DebuggerStepThrough]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.Synchronized)]
        private void AF(MetaType mT, FieldInfo f, Type t, ref int fI)
        {
            mT.Add(fI++, f.Name);
        }

        [DebuggerStepThrough]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.Synchronized)]
        private void SST(Type t, RuntimeTypeModel rM)
        {
            foreach (var rT in rM.GetTypes().Cast<MetaType>())
            {
                if (rT.Type != t.BaseType) continue;
                var sTI = rT.GetSubtypes().Length + S_P_I;
                rT.AddSubType(sTI, t);
                break;
            }
        }

        [DebuggerStepThrough]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.Synchronized)]
        private bool CFDMT(RuntimeTypeModel rM, Type t)
        {
            if (t == typeof(DeeplyMutableType) || t.BaseType == typeof(DeeplyMutableType))
            {
                rM.MakeDefault();
                _m = rM;
                return true;
            }
            return false;
        }

        [DebuggerStepThrough]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.Synchronized)]
        private void STP(MetaType mT, Type t, ref int fI)
        {
            foreach (var p in t.GetProperties())
            {
                try
                {
                    if (p.PropertyType.Name == typeof(object).Name)
                    {
                        continue;
                    }
                    AP(mT, p, t, ref fI);
                }
                catch
                {
                    continue;
                }
            }
        }

        [DebuggerStepThrough]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.Synchronized)]
        private void AP(MetaType mT, PropertyInfo p, Type t, ref int fI)
        {
            mT.Add(fI++, p.Name);
        }

        #region Change Serialization Model
        public enum CME
        {
            None,
            NullModel
        }
        [DebuggerStepThrough]
        [DebuggerNonUserCode]
        public readonly record struct CMO(CME Error);
        [DebuggerStepThrough]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.Synchronized)]
        public CMO ChangeModel(RuntimeTypeModel m)
        {
            if (m == null)
            {
                return new() { Error = CME.NullModel };
            }
            _m = m;
            _m.CompileInPlace();
            return new() { Error = CME.None };
        }
        #endregion Change Serialization Model
    }
}
#line default