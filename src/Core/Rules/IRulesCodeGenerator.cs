using Irony.Parsing;
using System.CodeDom;

namespace ByteCarrot.Masslog.Core.Rules
{
    public interface IRulesCodeGenerator
    {
        CodeCompileUnit GenerateCode(ParseTree tree);
    }
}