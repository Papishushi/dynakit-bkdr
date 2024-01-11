using System.Reflection;

namespace _.__._0x0005
{
    public static class ReflectiveEnumerator
    {
        public static IEnumerable<Type> GetEnumerableOfType<T>()
        {
            List<Type> types = new();
            foreach (var subclassType in Assembly.GetAssembly(typeof(T)).GetTypes().Where(x => x.IsSubclassOf(typeof(T))))
                types.Add(subclassType);
            return types;
        }
        public static IEnumerable<Type> GetEnumerableOfType(Type type)
        {
            List<Type> types = new();
            foreach (var subclassType in Assembly.GetAssembly(type).GetTypes().Where(x => x.IsSubclassOf(type)))
                types.Add(subclassType);
            return types;
        }
    }
}