using Microsoft.AspNetCore.Mvc;

namespace OurHouse.Extensions;

public static class UrlExtensions
{
    public static string? RouteUrlLang(this IUrlHelper urlHelper, string? routeName, string? languaje)
    {
        if (string.IsNullOrEmpty(routeName))
        {
            return urlHelper.RouteUrl("default");
        }

        string? url = urlHelper.RouteUrl($"{routeName}-{languaje}", new { lang = languaje });
        return url;
    }
}
