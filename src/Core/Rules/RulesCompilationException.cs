using System;

namespace ByteCarrot.Masslog.Core.Rules
{
    [Serializable]
    public class RulesCompilationException : ApplicationException
    {
        public RulesCompilationException(string message) : base(message)
        {
        }
    }
}