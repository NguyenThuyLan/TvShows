using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TvShows.Web.Common.Context;
using TvShows.Web.Models.Review;
using TvShows.Web.Models.ViewComponentModels;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Persistence.EFCore.Scoping;
using Umbraco.Cms.Web.Website.Controllers;

namespace TvShows.Web.Controllers
{
    public class TvShowReviewController : SurfaceController
	{
		private readonly ILogger<TvShowReviewController> _logger;

		private readonly IEFCoreScopeProvider<TvShowContext> _efCoreScopeProvider;
		public TvShowReviewController(
			ILogger<TvShowReviewController> logger,
			IEFCoreScopeProvider<TvShowContext> efCoreScopeProvider,
			IUmbracoContextAccessor umbracoContextAccessor,
			IUmbracoDatabaseFactory databaseFactory, 
			ServiceContext services,
			AppCaches appCaches,
			IProfilingLogger profilingLogger,
			IPublishedUrlProvider publishedUrlProvider)
			: base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
		{
			_logger = logger;
			_efCoreScopeProvider = efCoreScopeProvider;
		}

		[HttpPost]
		public async Task<IActionResult> Submit(Review review)
		{
			try
			{
				if (ModelState.IsValid == false)
				{
					return CurrentUmbracoPage();
				}

				TvShowReview tvShowReview = new TvShowReview()
				{
					TvShowKey = CurrentPage?.Key,
					Email = review.Email,
					UserName = review.UserName,
					Website = review.Website,
					Review = review.Message,
					TvShowTitle = CurrentPage?.Name ?? string.Empty
				};

				using IEfCoreScope<TvShowContext> scope = _efCoreScopeProvider.CreateScope();

				await scope.ExecuteWithContextAsync<Task>(async db =>
				{
					db.TvShowReviews.Add(tvShowReview);
					await db.SaveChangesAsync();
				});
				scope.Complete();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"TvShowReviewController:Submit {ex.Message}");
			}

			return RedirectToCurrentUmbracoPage();
        }
    }
}
