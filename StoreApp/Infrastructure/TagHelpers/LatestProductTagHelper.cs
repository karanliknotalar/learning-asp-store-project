using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Services.Contracts;

namespace StoreApp.Infrastructure.TagHelpers;

[HtmlTargetElement("div", Attributes = "products")]
public class LatestProductTagHelper : TagHelper
{
    private readonly IServiceManager _manager;

    [HtmlAttributeName("number")] public int Number { get; set; } = 5;

    public LatestProductTagHelper(IServiceManager manager)
    {
        _manager = manager;
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var products = _manager.ProductServices.GetLatestProduct(Number);

        var div = new TagBuilder("div");
        div.AddCssClass("my-3");

        var h6 = new TagBuilder("h6");
        h6.AddCssClass("lead");

        var i = new TagBuilder("i");
        i.AddCssClass("fa fa-box text-secondary");

        h6.InnerHtml.AppendHtml(i);
        h6.InnerHtml.AppendHtml(" Latest Products");

        var ul = new TagBuilder("ul");

        foreach (var product in products)
        {
            var li = new TagBuilder("li");
            li.AddCssClass("list-group-item");

            var a = new TagBuilder("a");
            a.AddCssClass("nav-link");
            a.Attributes.Add("href", $"/product/get/{product.ProductId}");
            a.InnerHtml.AppendHtml(product.ProductName ?? "");

            ul.InnerHtml.AppendHtml(li);
            li.InnerHtml.AppendHtml(a);
        }

        div.InnerHtml.AppendHtml(h6);
        div.InnerHtml.AppendHtml(ul);
        output.Content.AppendHtml(div);
        
    }
}