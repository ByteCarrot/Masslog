using ByteCarrot.Masslog.Core.Infrastructure.Extensions;
using Irony.Parsing;
using Microsoft.CSharp;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;

namespace ByteCarrot.Masslog.Core.Rules
{
    public class RulesCompiler : IRulesCompiler
    {
        private readonly IRulesCodeGenerator _generator;
        private readonly string _currentAssemblyFile;

        public bool DebugMode { get; set; }

        public RulesCompiler(IRulesCodeGenerator generator)
        {
            _generator = generator;
            _currentAssemblyFile = GetType().Assembly.GetName().Name + ".dll";
        }

        public CompilationResult Compile(string rules)
        {
            var interfaceName = typeof (IRule).Name;
            var tree = RulesGrammar.Parse(rules);
            if (tree.Status == ParseTreeStatus.Error)
            {
                return Errors(tree);
            }

            var unit = _generator.GenerateCode(tree);

            if (DebugMode)
            {
                Debug.WriteLine(unit.ToCSharp());
            }

            var result = Compile(unit);
            if (result.Errors.Count > 0)
            {
                return Errors(result);
            }

            var assembly = result.CompiledAssembly;
            var list = assembly.GetExportedTypes()
                .Where(x => x.IsClass && x.GetInterfaces().Any(i => i.Name == interfaceName))
                .Select(x => (IRule) assembly.CreateInstance(x.ToString()))
                .ToList();
            return new CompilationResult(list);
        }

        private static CompilationResult Errors(CompilerResults result)
        {
            var errors = result.Errors.Cast<CompilerError>().Select(x => new CompilationError(x)).ToList();
            return new CompilationResult(errors);
        }

        private static CompilationResult Errors(ParseTree tree)
        {
            return new CompilationResult(tree.ParserMessages.Select(x => new CompilationError(x)).ToList());
        }

        private CompilerResults Compile(CodeCompileUnit unit)
        {
            var lib = HttpContext.Current == null
                          ? GetType().Assembly.GetName().CodeBase
                          : HttpContext.Current.Server.MapPath("~/bin");
            var options = new CompilerParameters
            {
                GenerateInMemory = true,
                GenerateExecutable = false,
                OutputAssembly = Path.GetTempFileName(),
                CompilerOptions = "/lib:\"{0}\"".Args(lib)
            };
            options.ReferencedAssemblies.Add("System.dll");
            options.ReferencedAssemblies.Add(_currentAssemblyFile);

            return new CSharpCodeProvider().CompileAssemblyFromDom(options, unit);
        }
    }
}