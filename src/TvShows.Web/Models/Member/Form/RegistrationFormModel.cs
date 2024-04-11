using System.ComponentModel.DataAnnotations;

namespace TvShows.Web.Models.Member.Form
{
	public class RegistrationFormModel
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		public string StoreUserName { get; set; }

		[Required]
		[MinLength(10, ErrorMessage = "You password has to be at least 10 characters.")]
		public string Password { get; set; }

		[Required]
		[Compare("Password", ErrorMessage = "Your passwords do not match.")]
		public string PasswordRepeat { get; set; }
	}
}
