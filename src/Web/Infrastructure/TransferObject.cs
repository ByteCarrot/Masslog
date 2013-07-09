using System.Web.Mvc;

namespace ByteCarrot.Masslog.Web.Infrastructure
{
    public class TransferObject
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public JsonResult ToResult()
        {
            return new JsonResult {Data = this};
        }
    }
}