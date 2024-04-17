using Microsoft.Extensions.Logging;
using TvShows.Web.Common.Context;
using Umbraco.Cms.Persistence.EFCore.Scoping;
using Umbraco.UIBuilder.Configuration.Actions;
namespace TvShows.Web.Common.Actions
{
    public class ApproveTvShowReviewAction : Umbraco.UIBuilder.Configuration.Actions.Action<ActionResult>
    {
        private readonly IEFCoreScopeProvider<TvShowContext> _efCoreScopeProvider;
        private readonly ILogger<ApproveTvShowReviewAction> _logger;
        public override string Icon => "icon-check";
        public override string Alias => "approveaction";
        public override string Name => "Approve";
        public override bool ConfirmAction => true;

        public ApproveTvShowReviewAction(IEFCoreScopeProvider<TvShowContext> efCoreScopeProvider, ILogger<ApproveTvShowReviewAction> logger)
        {
            _efCoreScopeProvider = efCoreScopeProvider;
            _logger = logger;
        }

        public override ActionResult Execute(string collectionAlias, object[] entityIds)
        {
            ActionResult result = new ActionResult(true);
            try
            {
                if (entityIds?.Any() ?? false)
                {
                    using IEfCoreScope<TvShowContext> scope = _efCoreScopeProvider.CreateScope();

                    scope.ExecuteWithContextAsync<Task>(async db =>
                    {
                        var reviews = db.TvShowReviews.Where(x => entityIds.Contains(x.Id.ToString())).ToList();
                        if (reviews?.Any() ?? false)
                        {
                            foreach (var review in reviews)
                            {
                                review.IsApproved = true;
                            }
                            db.TvShowReviews.UpdateRange(reviews);
                            db.SaveChanges();
                        }
                    });
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                result = new ActionResult(false);
                _logger.LogError(ex, $"ApproveTvShowReviewAction: {ex.Message}");
            }
            return result;
        }
    }
}
