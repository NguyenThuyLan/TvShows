namespace TvShows.Web.Models.Review
{
	public class CommentModel
	{
        public int Total { get; set; }

		public IEnumerable<TvShowReview> Reviews { get; set; }
    }
}
