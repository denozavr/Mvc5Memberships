using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mvc5Memberships.Extenstion
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString GlyphLink(this HtmlHelper htmlHelper, string controller, string action,
            string text,string glyphicon, string cssClasses = "", string id = "")
        {
            // declare a span for the glyphicon
            var glyph = $"<span class='glyphicon glyphicon-{glyphicon}'></span>";

            // declare the anchor tag
            var anchor = new TagBuilder("a");
            anchor.MergeAttribute("href", controller.Length > 0 ? $"/{controller}/{action}/" : "#");


            anchor.InnerHtml = $"{glyph} {text}";
            anchor.AddCssClass(cssClasses);
            anchor.GenerateId(id);

            return MvcHtmlString.Create(anchor.ToString(TagRenderMode.Normal));
        }

    }
}