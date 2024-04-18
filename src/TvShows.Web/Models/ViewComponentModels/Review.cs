using System.ComponentModel.DataAnnotations;

namespace TvShows.Web.Models.ViewComponentModels
{
	public sealed class Review
	{
		[Display(Name = "Name")]
        [Required]
        public string UserName { get; set; }
		[Required]
		public string Email { get; set; }
		[Required]
		public string Website { get; set; }
		[Required]
		public string Message { get; set; }
    }
}
