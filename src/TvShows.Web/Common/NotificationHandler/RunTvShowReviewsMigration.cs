using Microsoft.EntityFrameworkCore;
using TvShows.Web.Common.Context;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;

namespace TvShows.Web.Common.NotificationHandler
{
    public class RunTvShowReviewsMigration : INotificationAsyncHandler<UmbracoApplicationStartedNotification>
    {
        private readonly TvShowContext _tvShowContext;

        public RunTvShowReviewsMigration(TvShowContext tvShowContext)
        {
            _tvShowContext = tvShowContext;
        }

        public async Task HandleAsync(UmbracoApplicationStartedNotification notification, CancellationToken cancellationToken)
        {
            IEnumerable<string> pendingMigrations = await _tvShowContext.Database.GetPendingMigrationsAsync();

            if (pendingMigrations.Any())
            {
                await _tvShowContext.Database.MigrateAsync();
            }
        }
    }
}
