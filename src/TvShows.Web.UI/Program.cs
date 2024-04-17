
using Hangfire;
using Microsoft.Extensions.Options;
using TvShows.Web.Models;
using TvShows.Web.Utility;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.CreateUmbracoBuilder()
    .AddBackOffice()
    .AddWebsite()
    .AddDeliveryApi()
    .AddComposers()
    .Build();
if (builder.Environment.IsProduction())
{
    RecurringJob.AddOrUpdate<TvShowService>("MoveOneTvShowFromTvMazeToUmbraco", x => x.MoveTvShowsFromTvMazeToUmbraco(), Cron.Monthly);
}

builder.Services.Configure<Appsettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<Appsettings>>().Value);

WebApplication app = builder.Build();

// Making a change so that it deploys
await app.BootUmbracoAsync();

app.UseHttpsRedirection();

app.UseUmbraco()
    .WithMiddleware(u =>
    {
        u.UseBackOffice();
        u.UseWebsite();
    })
    .WithEndpoints(u =>
    {
        u.UseInstallerEndpoints();
        u.UseBackOfficeEndpoints();
        u.UseWebsiteEndpoints();
    });

await app.RunAsync();
