using System.ComponentModel.DataAnnotations;

namespace TvShows.Web.Models.Member.Form
{
	public class RegistrationFormModel
	{
		[Required]
		[EmailAddress]
		[Display(Name = "Your Email")]
		public string Email { get; set; }

		[Required]
        [Display(Name = "Name")]
        public string StoreUserName { get; set; }

		[Required]
		[MinLength(10, ErrorMessage = "You password has to be at least 10 characters.")]
        [Display(Name = "Password")]
        public string Password { get; set; }

		[Required]
		[Compare("Password", ErrorMessage = "Your passwords do not match.")]
        [Display(Name = "Password Repeat")]
        public string PasswordRepeat { get; set; }
	}
}
