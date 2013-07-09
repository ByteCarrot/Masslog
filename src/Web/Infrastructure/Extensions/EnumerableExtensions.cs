using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ByteCarrot.Masslog.Web.Infrastructure.Extensions
{
    public static class EnumerableExtensions
    {
        public static List<SelectListItem> ToSelectList(this IEnumerable<string> enumerable, string selectedValue = "", bool addEmptyItem = true)
        {
            var list = enumerable.Select(x => CreateSelectListItem(x, x, selectedValue)).ToList();
            if (addEmptyItem)
            {
                list.Insert(0, new SelectListItem {Selected = selectedValue == String.Empty, Text = String.Empty, Value = String.Empty});
            }
            return list;
        }

        public static List<SelectListItem> ToSelectList<T>(this IEnumerable<T> enumerable, Func<T, string> text, Func<T, object> value, object selectedValue = null, bool addEmptyItem = true)
        {
            var svalue = selectedValue == null ? String.Empty : selectedValue.ToString();
            var list = enumerable.Select(x => CreateSelectListItem(text(x), value(x).ToString(), svalue)).ToList();
            
            if (addEmptyItem)
            {
                list.Insert(0, new SelectListItem { Selected = (string) selectedValue == String.Empty, Text = String.Empty, Value = String.Empty });
            }
            return list;
        }

        private static SelectListItem CreateSelectListItem(string text, string value, string selectedValue)
        {
            return new SelectListItem
                       {
                           Selected = value == selectedValue,
                           Text = text,
                           Value = value
                       };
        }
    }
}