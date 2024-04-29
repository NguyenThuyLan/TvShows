namespace TvShows.Web.Models
{
	public class ShowModel
	{
		public int TvShowID { get; set; }
		public Guid? TvShowGuidId { get; set; }
		public string ShowTitle { get; set; }
		public string? Summary { get; set; }
		public DateTime Premiered { get; set; }
		public string? PreImage { get; set; }
		public bool CreatedByForm { get; set; } = false;
	}
}
