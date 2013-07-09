using System.Collections.Generic;
using System.Linq;
using ByteCarrot.Masslog.Core.DomainModel.Entities;

namespace ByteCarrot.Masslog.Web.Controllers.Activities
{
    public static class NameValueExtensions
    {
        public static List<NameValueListViewModel> ToNameValueListViewModels(this List<NameValue> list)
        {
            return list == null 
                ? new List<NameValueListViewModel>() 
                : list.OrderBy(h => h.Name).Select(h => new NameValueListViewModel(h.Name, h.Value)).ToList();
        }
    }
}