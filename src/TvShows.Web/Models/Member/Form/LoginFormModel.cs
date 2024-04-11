using System.ComponentModel.DataAnnotations;

namespace TvShows.Web.Models.Member.Form
{
	public class LoginFormModel
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		public string Password { get; set; }
	}
}
