using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StoreApp.Infrastructure.TagHelpers;

[HtmlTargetElement("a")]
[HtmlTargetElement("button")]
public class ToolTipTagHelper : TagHelper
{
    public string ToolMessage { get; set; } = string.Empty;

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (!string.IsNullOrEmpty(ToolMessage))
        {
            output.Attributes.SetAttribute("data-bs-toggle", "tooltip");
            output.Attributes.SetAttribute("data-bs-placement", "top");
            output.Attributes.SetAttribute("data-bs-custom-class", "custom-tooltip");
            output.Attributes.SetAttribute("data-bs-title", ToolMessage);
        }
        base.Process(context, output);
    }
}