using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;

namespace FeastBook_final.Code
{
    public static class MyHelpers
    {
        public static IHtmlContent Image(this IHtmlHelper helper, string src, string altText, string height)
        {
            var builder = new TagBuilder("img");
            builder.MergeAttribute("src", src);
            builder.MergeAttribute("alt", altText);
            builder.MergeAttribute("height", height);
            string result;
            using (var writer = new StringWriter())
            {
                builder.WriteTo(writer, System.Text.Encodings.Web.HtmlEncoder.Default);
                result = writer.ToString();
            }
            return new HtmlString(result);
        }
        public static IHtmlContent Image(this IHtmlHelper helper, string src, string altText)
        {
            var builder = new TagBuilder("img"); builder.MergeAttribute("src", src);
            builder.MergeAttribute("alt", altText);
            string result;
            using (var writer = new StringWriter())
            {
                builder.WriteTo(writer, System.Text.Encodings.Web.HtmlEncoder.Default);
                result = writer.ToString();
            }
            return new HtmlString(result);
        }
    }
}
