using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace E_Commercial.UI.Custom_Tag_Helpers
{
    [HtmlTargetElement("product-list-pager")]
    public class PagingTagHelper : TagHelper
    {
        [HtmlAttributeName("page-count")]
        public int PageCount { get; set; }

        [HtmlAttributeName("page-size")]
        public int PageSize { get; set; }

        [HtmlAttributeName("current-category-id")]
        public int CurrentCategoryId { get; set; }

        [HtmlAttributeName("current-page")]
        public int CurrentPage { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<nav>");
            stringBuilder.Append("<ul class='pagination'>");

            // if available previous pages
            if (CurrentPage != 1)
            {
                stringBuilder.AppendFormat("<li class='page-item'><a class='page-link' href='{0}'>Previous</a></li>", "/product/index/?page=" + (CurrentPage - 1) + "&CategoryId=" + CurrentCategoryId);
            }

            for (int i = 1; i <= PageCount; i++)
            {
                stringBuilder.AppendFormat("<li class='page-item {0}'><a class='page-link' href='{1}'>{2}</a></li>", i == CurrentPage ? "active" : " ", "/product/index/?page=" + i + "&categoryId=" + CurrentCategoryId, i);
            }

            // if available next pages
            if (CurrentPage < PageCount)
            {
                stringBuilder.AppendFormat("<li class='page-item'><a class='page-link' href='{0}'>Next</a></li>", "/product/index/?page=" + (CurrentPage + 1) + "&CategoryId=" + CurrentCategoryId);
            }

            stringBuilder.Append("</ul>");
            stringBuilder.Append("</nav>");

            output.Content.SetHtmlContent(stringBuilder.ToString());
            base.Process(context, output);
        }
    }
}
