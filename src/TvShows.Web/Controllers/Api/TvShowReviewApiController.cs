using Microsoft.AspNetCore.Mvc;
using Serilog.Context;
using TvShows.Web.Common.Context;
using TvShows.Web.Models.Review;
using Umbraco.Cms.Persistence.EFCore.Scoping;
using Umbraco.Cms.Web.Common.Controllers;

namespace TvShows.Web.Controllers
{
    public class TvShowReviewApiController : UmbracoApiController
    {
        private readonly IEFCoreScopeProvider<TvShowContext> _efCoreScopeProvider;

        public TvShowReviewApiController(IEFCoreScopeProvider<TvShowContext> efCoreScopeProvider)
        {
            _efCoreScopeProvider = efCoreScopeProvider;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            using IEfCoreScope<TvShowContext> scope = _efCoreScopeProvider.CreateScope();
            IEnumerable<TvShowReview> reviews = await scope.ExecuteWithContextAsync(async db => db.TvShowReviews.ToArray());
            scope.Complete();
            return Ok(reviews);
        }

        [HttpGet]
        public async Task<IActionResult> GetReviews(Guid umbracoNodeKey)
        {
            using IEfCoreScope<TvShowContext> scope = _efCoreScopeProvider.CreateScope();
            IEnumerable<TvShowReview> reviews = await scope.ExecuteWithContextAsync(async db =>
            {
                return db.TvShowReviews.Where(x => x.TvShowKeyId == umbracoNodeKey).ToArray();
            });

            scope.Complete();
            return Ok(reviews);
        }

        [HttpPost]
        public async Task InsertReview([FromBody] TvShowReview review)
        {
            using IEfCoreScope<TvShowContext> scope = _efCoreScopeProvider.CreateScope();

            await scope.ExecuteWithContextAsync<Task>(async db =>
            {
                db.TvShowReviews.Add(review);
                await db.SaveChangesAsync();
            });

            scope.Complete();
        }
    }
}
