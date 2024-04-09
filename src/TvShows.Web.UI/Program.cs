
using Hangfire;
using TvShows.Web.Utility;
using Umbraco.Cms.Infrastructure.Persistence.DatabaseModelDefinitions;
using static Umbraco.Cms.Core.PropertyEditors.ImageCropperConfiguration;

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
System.Console.WriteLine("Hello");
WebApplication app = builder.Build();

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
