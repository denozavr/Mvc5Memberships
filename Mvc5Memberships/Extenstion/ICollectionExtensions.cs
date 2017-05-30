using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc5Memberships.Models;

namespace Mvc5Memberships.Extenstion
{
    public static class ICollectionExtensions
    {
        public static IEnumerable<SelectListItem> ToSelectListItem<T>(this ICollection<T> items, int selected)
        {
            //cannot get props directly because of type T
            //so I used reflection(our Reflection Extension method) to solve this
            return items.Select(x => new SelectListItem()
            {
                Text = x.GetPropValue("Title"),
                Value = x.GetPropValue("Id"),
                Selected = x.GetPropValue("Id").Equals(selected.ToString())
            });

            //Alternative syntax
            //return from item in items
            //    select new SelectListItem()
            //    {
            //        Text = item.GetPropValue("Title"),
            //        Value = item.GetPropValue("Id"),
            //        Selected = item.GetPropValue("Id").Equals(selected.ToString())
            //    };
        }

        public static string ShowItemType(this int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var tit = db.ItemTypes.FirstOrDefault(x => x.Id == id)?.Title ?? "NULL";

            return tit;
        }

    }
}