using ByteCarrot.Masslog.Core.Rules;
using ByteCarrot.Masslog.Web.Infrastructure;

namespace ByteCarrot.Masslog.Web.Controllers.Applications
{
    public class VerifyTransferObject : TransferObject
    {
        public int Line { get; set; }

        public int Char { get; set; }

        public static VerifyTransferObject Create(ParseResult result)
        {
            return new VerifyTransferObject
                       {
                           Success = result.Success,
                           Message = result.Message,
                           Line = result.Line,
                           Char = result.Char
                       };
        }

        public static VerifyTransferObject EmptyRules()
        {
            return new VerifyTransferObject {Success = true};
        }
    }
}