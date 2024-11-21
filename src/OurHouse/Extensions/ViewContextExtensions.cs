﻿using Microsoft.AspNetCore.Mvc.Rendering;

namespace OurHouse.Extensions;

public static class ViewContextExtensions
{
    public static string GetLang(this ViewContext context)
    {
        string? lang = context.RouteData.Values["lang"]?.ToString();
        return lang ?? "es";
    }
}