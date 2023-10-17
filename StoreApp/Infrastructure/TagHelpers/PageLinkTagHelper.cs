using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using StoreApp.Models;

namespace StoreApp.Infrastructure.TagHelpers;

[HtmlTargetElement("div", Attributes = "pagination")]
public class PageLinkTagHelper : TagHelper
{
    private readonly IUrlHelperFactory _urlHelperFactory;

    [ViewContext] [HtmlAttributeNotBound] public ViewContext? ViewContext { get; set; }
    public Pagination? Pagination { get; set; }
    public string? PageAction { get; set; }
    public bool BtnClassEnabled { get; set; } = false;
    public string BtnClass { get; set; } = string.Empty;
    public string BtnClassStyleNormal { get; set; } = string.Empty;
    public string BtnClassStyleSelected { get; set; } = string.Empty;

    public PageLinkTagHelper(IUrlHelperFactory urlHelperFactory)
    {
        _urlHelperFactory = urlHelperFactory;
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (ViewContext is not null && Pagination is not null)
        {
            var urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);

            var mainDiv = new TagBuilder("div");

            for (var i = 1; i <= Pagination.TotalPages; i++)
            {
                var a = new TagBuilder("a")
                {
                    Attributes =
                    {
                        ["href"] = urlHelper.Action(PageAction, new { PageNumber = i })
                    }
                };

                var btnClass = Pagination.CurrentPage == i ? BtnClassStyleSelected : BtnClassStyleNormal;
                if (BtnClassEnabled) a.AddCssClass($"{BtnClass} {btnClass}");

                a.InnerHtml.Append(i.ToString());

                mainDiv.InnerHtml.AppendHtml(a);
            }

            output.Content.AppendHtml(mainDiv.InnerHtml);
        }
    }
}