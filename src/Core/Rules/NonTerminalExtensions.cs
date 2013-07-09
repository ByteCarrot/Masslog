using Irony.Parsing;

namespace ByteCarrot.Masslog.Core.Rules
{
    public static class NonTerminalExtensions
    {
        public static NonTerminal WithRule(this NonTerminal @this, BnfExpression expression)
        {
            @this.Rule = expression;
            return @this;
        }
    }
}