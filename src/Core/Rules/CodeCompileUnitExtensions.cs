using ByteCarrot.Masslog.Core.Infrastructure.Extensions;
using Microsoft.CSharp;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;

namespace ByteCarrot.Masslog.Core.Rules
{
    public static class CodeCompileUnitExtensions
    {
        public static string ToCSharp(this CodeCompileUnit unit)
        {
            using (var stream = new MemoryStream())
            {
                var csharp = new CSharpCodeProvider();

                var writer = new IndentedTextWriter(new StreamWriter(stream));
                csharp.GenerateCodeFromCompileUnit(unit, writer, new CodeGeneratorOptions());
                writer.Flush();

                return stream.ReadToEnd();
            }
        }
    }
}