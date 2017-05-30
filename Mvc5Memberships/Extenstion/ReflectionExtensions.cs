using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc5Memberships.Extenstion
{
    public static class ReflectionExtensions
    {
        public static string GetPropValue<T>(this T item, string propertyName)
        {
            return item.GetType().GetProperty(propertyName)?.GetValue(item, null).ToString();
        }
    }
}