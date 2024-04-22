using Microsoft.AspNetCore.Mvc;
using TvShows.Web.Common.Context;
using TvShows.Web.Models.Review;
using TvShows.Web.Models.ViewComponentModels;
using Umbraco.Cms.Persistence.EFCore.Scoping;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace TvShows.Web.ViewComponents
{
	[ViewComponent]
	public class CommentsViewComponent : ViewComponent
	{
		private readonly IEFCoreScopeProvider<TvShowContext> _efCoreScopeProvider;

		public CommentsViewComponent(IEFCoreScopeProvider<TvShowContext> efCoreScopeProvider)
        {
            _efCoreScopeProvider = efCoreScopeProvider;
        }
        public IViewComponentResult Invoke(TvShow model)
		{
			using IEfCoreScope<TvShowContext> scope = _efCoreScopeProvider.CreateScope();
			IEnumerable<Review> reviews = new List<Review>();
			IEnumerable<ReviewModel> reviewModels = new List<ReviewModel>();
			CommentModel comment = new CommentModel();

			scope.ExecuteWithContextAsync<Task>(async db =>
			{
				reviews = db.Reviews.Where(x => x.TvShowKeyId == model.Key && x.IsApproved == true).ToArray();
				if(reviews.Any())
				{
					comment.Total = reviews.Count();
					comment.Reviews = reviews;
				}
			});
			scope.Complete();

			return View(comment);
		}
	}
}
