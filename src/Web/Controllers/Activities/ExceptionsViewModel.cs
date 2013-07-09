using System.Collections.Generic;
using ByteCarrot.Masslog.Web.Infrastructure;

namespace ByteCarrot.Masslog.Web.Controllers.Activities
{
    public class ExceptionsViewModel : ViewModel
    {
        public string Id { get; set; }

        public List<ExceptionViewModel> Exceptions { get; set; }
    }
}