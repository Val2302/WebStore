using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using WebStore.Domain.Models;

namespace WebStore.Helpers.TagHelpers
{
    public class PagingTagHelper : TagHelper
    {
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        public PageViewModel PageModel { get; set; }
        public string PageAction { get; set; }

        [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
        public Dictionary<string, object> PageUrlValues { get; set; } = new Dictionary<string, object>();

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var tag = new TagBuilder("ul");
            tag.AddCssClass("pagination");

            for (var i = 1; i <= PageModel.TotalPages; i++)
            {
                var item = CreateTag(i);
                tag.InnerHtml.AppendHtml(item);
            }

            base.Process(context, output);

            output.Content.AppendHtml(tag);
        }

        private TagBuilder CreateTag(int pageNumber)
        {
            var item = new TagBuilder("li");
            var link = new TagBuilder("a");

            if (pageNumber == PageModel.PageNumber)
            {
                link.MergeAttribute("data-page", PageModel.PageNumber.ToString());
                item.AddCssClass("active");
            }
            else
            {
                PageUrlValues["page"] = pageNumber;
                link.Attributes["href"] = "#";
                foreach (var pageUrlValue in PageUrlValues)
                {
                    if (pageUrlValue.Value is null) continue;
                    
                    link.MergeAttribute($"data-{pageUrlValue.Key}", pageUrlValue.Value.ToString());
                }
            }

            link.InnerHtml.Append(pageNumber.ToString());
            item.InnerHtml.AppendHtml(link);

            return item;
        }
    }
}
