
using Hangfire;
using Microsoft.Extensions.Options;
using TvShows.Web.Common.Actions;
using TvShows.Web.Models;
using TvShows.Web.Models.Review;
using TvShows.Web.Utility;
using Umbraco.UIBuilder.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.CreateUmbracoBuilder()
    .AddBackOffice()
    .AddWebsite()
    .AddDeliveryApi()
    .AddUIBuilder(cfg =>
    {
        cfg.AddSectionAfter("media", "Repositories", sectionConfig => sectionConfig
        .Tree(treeConfig => treeConfig
            .AddCollection<Review>(x => x.Id, "TvShowReview", "TvShowReviews", "A person entity", "icon-umb-users", "icon-umb-users", collectionConfig => collectionConfig
                .AddAction<ApproveTvShowReviewAction>(actionConfig => actionConfig
                    .SetVisibility(x => x.ActionType == Umbraco.UIBuilder.Configuration.Actions.ActionType.Bulk
                        || x.ActionType == Umbraco.UIBuilder.Configuration.Actions.ActionType.Row)
                    )
                .SetNameProperty(p => p.UserName)
                .ListView(listViewConfig => listViewConfig
                    .AddField(p => p.Email)
                    .AddField(p => p.TvShowTitle, fieldConfig =>
                    {
                        fieldConfig.SetHeading("TvShow");
                    })
                    .AddField(p => p.Message)
                    .AddField(p => p.IsApproved, fieldConfig =>
                    {
                        fieldConfig.SetHeading("Approved");
                    })
                    .AddField(p => p.CreatedDate)
                )
                //.Editor(editorConfig => editorConfig
                //    .AddTab("General", tabConfig => tabConfig
                //        .AddFieldset("General", fieldsetConfig => fieldsetConfig
                //            .AddField(p => p.Email).SetValidationRegex("[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+")
                //            //.AddField(p=>p.Website)
                //            .AddField(p => p.Message)
                //            .AddField(p => p.TvShowKeyId).SetValidationRegex("[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}")
                //        )
                //    //.AddFieldset("Media", fieldsetConfig => fieldsetConfig
                //    //    .AddField(p => p.Avatar).SetDataType("Upload File")
                //    //)
                //    )
                //)
                // 
            )
        )
    );

    })
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
