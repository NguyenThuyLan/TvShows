using Microsoft.AspNetCore.Mvc;
using TvShows.Web.Models.ViewComponentModels;

namespace TvShows.Web.ViewComponents
{
    [ViewComponent]
    public class TvShowReviewViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View(new ReviewModel());
        }
    }
}
