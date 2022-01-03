using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace QLCHVT.Models
{
    public static class ImageHelper
    {
        public static MvcHtmlString Image(
           this HtmlHelper helper, string src, string Width, string height, string alt)
        {
            var builder = new TagBuilder("img");
            builder.MergeAttribute("src", src);
            builder.MergeAttribute("width", Width);
            builder.MergeAttribute("height", height);
            builder.MergeAttribute("alt", alt);
            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
        }
    }
}