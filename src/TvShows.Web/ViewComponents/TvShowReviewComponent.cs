using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvShows.Web.Models.Review;

namespace TvShows.Web.ViewComponents
{
    public class TvShowReviewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(TvShowReview review)
        {
            return View(review);
        }
    }
}
