namespace ByteCarrot.Masslog.Core.Rules
{
    public interface IRulesParser
    {
        ParseResult Parse(string rules);
    }
}