using System.Collections.Generic;
using ByteCarrot.Masslog.Web.Infrastructure;

namespace ByteCarrot.Masslog.Web.Controllers.Activities
{
    public class ServerVariablesViewModel : ViewModel
    {
        public string Id { get; set; }

        public bool VariablesIgnored { get; set; }

        public List<NameValueListViewModel> Variables { get; set; }
    }
}