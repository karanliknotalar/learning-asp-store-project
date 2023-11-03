using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Services.Contracts;
using StoreApp.Models;

namespace StoreApp.Infrastructure.TagHelpers;

[HtmlTargetElement("td", Attributes = "user-role")]
public class UserRoleTagHelper : TagHelper
{
    //[HtmlAttributeName("user-name")] olamasa da olur?
    [HtmlAttributeName("user-name")] public string? UserName { get; set; } = string.Empty;

    private readonly IServiceManager _manager;

    public UserRoleTagHelper(IServiceManager manager)
    {
        _manager = manager;
    }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        // throw new Exception("username:" + UserName);
        if (UserName is not null)
        {
            var roles = _manager.AuthService.Roles.Select(r => r.Name).ToList();
            
            if (roles.Any())
            {
                var ul = new TagBuilder("ul");
                foreach (var role in roles)
                {
                    var isInRole = await _manager.AuthService.UserIsInRoleAsync(UserName, role);
                    var li = new TagBuilder("li");
                    li.AddCssClass($"list-group-item {(isInRole ? "text-success" : "text-danger")}");
                    li.InnerHtml.Append($"{role}: {(isInRole ? "Active" : "Passive")}");
                    ul.InnerHtml.AppendHtml(li);
                }
                output.Content.AppendHtml(ul.InnerHtml);
            }
        }
    }
}