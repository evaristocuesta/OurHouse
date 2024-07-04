using AspNetStatic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IStaticResourcesInfoProvider>(
  new StaticResourcesInfoProvider(
    [
      new PageResource("/"),
      new PageResource("/home/OurHouse"),
      new CssResource("/lib/bootstrap/dist/css/bootstrap.min.css") { OptimizerType = OptimizerType.None },
      new CssResource("/css/site.css?v=pAGv4ietcJNk_EwsQZ5BN9-K4MuNYS2a9wl4Jw-q9D0"),
      new CssResource("/OurHouse.styles.css?v=QVIm3G0TQnz7jhf0QoO7Vxi4Cck3I2ZBcZUJUpvQ19o"),
      new JsResource("/lib/bootstrap/dist/js/bootstrap.bundle.min.js"),
      new JsResource("/js/site.js?v=hRQyftXiu1lLX2P9Ly9xa4gHJgLeR1uGN5qegUobtGo")]
    ));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


if (args.HasExitWhenDoneArg())
{
    var outputPath = $"{app.Environment.ContentRootPath}/bin/web-output";

    if (!Path.Exists(outputPath))
    {
        Console.WriteLine($"Creating directory {outputPath}");
        Directory.CreateDirectory(outputPath);
    }

    Console.WriteLine($"Generating static content in {outputPath}");

    app.GenerateStaticContent(outputPath,
        alwaysDefaultFile: true,
        exitWhenDone: true);
}

await app.RunAsync();
