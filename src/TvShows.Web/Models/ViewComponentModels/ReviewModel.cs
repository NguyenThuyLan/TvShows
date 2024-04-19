using System.ComponentModel.DataAnnotations;

namespace TvShows.Web.Models.ViewComponentModels
{
	public sealed class ReviewModel
	{
		[Required]
		public string Message { get; set; }
    }
}
