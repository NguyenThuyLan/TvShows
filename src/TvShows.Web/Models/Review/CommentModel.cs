namespace TvShows.Web.Models.Review
{
	public class CommentModel
	{
        public int Total { get; set; }

		public IEnumerable<Review> Reviews { get; set; }
    }
}
