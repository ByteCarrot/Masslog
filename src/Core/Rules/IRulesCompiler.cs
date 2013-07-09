
namespace ByteCarrot.Masslog.Core.Rules
{
    public interface IRulesCompiler
    {
        CompilationResult Compile(string rules);
    }
}