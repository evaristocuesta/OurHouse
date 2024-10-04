using AspNetStatic;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using OurHouse.Pipelines;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var supportedCultures = new[]
{
    new CultureInfo("en"),
    new CultureInfo("es"),
};

var options = new RequestLocalizationOptions()
{
    DefaultRequestCulture = new RequestCulture(culture: "en", uiCulture: "en"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
};

options.RequestCultureProviders =
[
    new RouteDataRequestCultureProvider() { Options = options, RouteDataStringKey = "lang" }
];

builder.Services.AddSingleton(options);

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddMvc(opts =>
{
    opts.Filters.Add(new MiddlewareFilterAttribute(typeof(LocalizationPipeline)));
})
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);

var outputPath = args.Length >= 2 ? $"{args[1]}" : string.Empty;
var basePath = args.Length == 3 ? $"/{args[2]}" : string.Empty;

builder.Services.AddSingleton<IStaticResourcesInfoProvider>(
  new StaticResourcesInfoProvider(
    [
      new PageResource($"{basePath}/"),
      new PageResource($"{basePath}/es"),
      new PageResource($"{basePath}/en"),
      new PageResource($"{basePath}/es/nuestra-casa"),
      new PageResource($"{basePath}/en/our-house"),
      new PageResource($"{basePath}/es/contacto"),
      new PageResource($"{basePath}/en/contact"),
      new PageResource($"{basePath}/es/descubre-cortelazor"),
      new PageResource($"{basePath}/en/discover-cortelazor"),
      new PageResource($"{basePath}/en/error/404") { OutFile = $"{basePath}/404.html"},
      new CssResource($"{basePath}/css/site.css?v=pAGv4ietcJNk_EwsQZ5BN9-K4MuNYS2a9wl4Jw-q9D0"),
      new CssResource($"{basePath}/OurHouse.styles.css?v=QVIm3G0TQnz7jhf0QoO7Vxi4Cck3I2ZBcZUJUpvQ19o"),
      new JsResource($"{basePath}/js/site.js?v=hRQyftXiu1lLX2P9Ly9xa4gHJgLeR1uGN5qegUobtGo"), 
      new BinResource($"{basePath}/fox.svg"),
      new BinResource($"{basePath}/casita-del-zorro.svg")]
    ));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/en/error/{0}");

app.UseHttpsRedirection();
app.UseStaticFiles(basePath);

app.UsePathBase(basePath);
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "Index-en",
    pattern: "en",
    defaults: new { lang = "en", controller = "Home", action = "Index" },
    constraints: new { lang = @"(\w{2})" });

app.MapControllerRoute(
    name: "Index-es",
    pattern: "es",
    defaults: new { lang = "es", controller = "Home", action = "Index" },
    constraints: new { lang = @"(\w{2})" });

app.MapControllerRoute(
    name: "OurHouse-en",
    pattern: "{lang=en}/our-house",
    defaults: new { lang = "en", controller = "Home", action = "OurHouse"},
    constraints: new { lang = @"(\w{2})" });

app.MapControllerRoute(
    name: "OurHouse-es",
    pattern: "{lang=es}/nuestra-casa",
    defaults: new { lang = "es", controller = "Home", action = "OurHouse" },
    constraints: new { lang = @"(\w{2})" });

app.MapControllerRoute(
    name: "Contact-en",
    pattern: "{lang=en}/contact",
    defaults: new { lang = "en", controller = "Home", action = "Contact" },
    constraints: new { lang = @"(\w{2})" });

app.MapControllerRoute(
    name: "Contact-es",
    pattern: "{lang=es}/contacto",
    defaults: new { lang = "es", controller = "Home", action = "Contact" },
    constraints: new { lang = @"(\w{2})" });

app.MapControllerRoute(
    name: "DiscoverCortelazor-en",
    pattern: "{lang=en}/discover-cortelazor",
    defaults: new { lang = "en", controller = "Home", action = "DiscoverCortelazor" },
    constraints: new { lang = @"(\w{2})" });

app.MapControllerRoute(
    name: "DiscoverCortelazor-es",
    pattern: "{lang=es}/descubre-cortelazor",
    defaults: new { lang = "es", controller = "Home", action = "DiscoverCortelazor" },
    constraints: new { lang = @"(\w{2})" });

app.MapControllerRoute(
    name: "Error",
pattern: "{lang=en}/error/{statusCode}",
    defaults: new { lang = "en", controller = "Home", action = "Error", statusCode = 0},
    constraints: new { lang = @"(\w{2})" });


app.MapControllerRoute(
    name: "default",
    pattern: "{lang=en}/{controller=Home}/{action=Index}/{id?}",
    constraints: new { lang = @"(\w{2})" });


if (args.HasExitWhenDoneArg())
{
    if (!Path.Exists(outputPath))
    {
        Console.WriteLine($"Creating directory {outputPath}");
        Directory.CreateDirectory(outputPath);
    }

    Console.WriteLine($"Generating static content in {outputPath}");

    app.GenerateStaticContent(outputPath,
        alwaysDefaultFile: true,
        exitWhenDone: true, 
        dontUpdateLinks: true);
}

await app.RunAsync();
