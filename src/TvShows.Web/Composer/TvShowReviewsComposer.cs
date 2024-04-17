using TvShows.Web.Common.NotificationHandler;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Notifications;

namespace TvShows.Web.Composer
{
    public class TvShowReviewsComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder) => builder.AddNotificationAsyncHandler<UmbracoApplicationStartedNotification, RunTvShowReviewsMigration>();
    }
}
