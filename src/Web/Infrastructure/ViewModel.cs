using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ByteCarrot.Masslog.Web.Infrastructure
{
    public class ViewModel
    {
        protected List<SelectListItem> EnumToSelectList<TEnum>()
        {
            return Enum.GetNames(typeof(TEnum))
                .Select(x => new SelectListItem
                                 {
                                     Selected = false,
                                     Text = x,
                                     Value = Enum.Parse(typeof(TEnum), x).ToString()
                                 }).ToList();
        }
    }
}