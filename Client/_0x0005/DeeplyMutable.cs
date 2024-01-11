using System.Diagnostics;
using System.Reflection;

#line hidden
namespace _.__._0x0005
{
    [DebuggerStepThrough]
    [DebuggerNonUserCode]
    public sealed class DeeplyMutable<T> : DeeplyMutableType
    {
        public new T? Value { get => _value; set => _value = value; }
        public DeeplyMutable() : base() { }
        public DeeplyMutable(T? value) : base(value) { }
        public DeeplyMutable(DeeplyMutableType deeplyMutableType) => _value = deeplyMutableType.Value;

        protected override FieldInfo[]? DynamicFields => typeof(T).GetFields();
    }
}
#line default