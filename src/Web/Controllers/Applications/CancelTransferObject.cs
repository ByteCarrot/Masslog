using ByteCarrot.Masslog.Web.Infrastructure;

namespace ByteCarrot.Masslog.Web.Controllers.Applications
{
    public class CancelTransferObject : TransferObject
    {
        public string Rules { get; set; }
    }
}