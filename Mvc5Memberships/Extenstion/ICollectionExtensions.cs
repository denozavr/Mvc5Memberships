using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mvc5Memberships.Extenstion
{
    public static class ICollectionExtensions
    {
        public static IEnumerable<SelectListItem> ToSelectListItem<T>(this ICollection<T> items, int selected)
        {
            return from item in items
                select new SelectListItem()
                {
                    Text = item.GetPropValue("Title"),
                    Value = item.GetPropValue("Id"),
                    Selected = item.GetPropValue("Id").Equals(selected.ToString())
                };
        }
    }
}