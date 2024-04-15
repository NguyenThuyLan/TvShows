using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TvShows.Web.Models.Member.Form
{
	public class RegistrationFormModel
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
        [DisplayName("Name")]
        public string StoreUserName { get; set; }

		[Required]
		[MinLength(10, ErrorMessage = "You password has to be at least 10 characters.")]
        public string Password { get; set; }

		[Required]
		[Compare("Password", ErrorMessage = "Your passwords do not match.")]
        public string PasswordRepeat { get; set; }

        #region labelString
        public string? LangCulture { get; set; }
        public string? TitleForm { get; set; }
        public string? NameLabel { get; set; }
        public string? EmailLabel { get; set; }
        public string? PasswordLabel { get; set; }
        public string? PasswordRepeatLabel { get; set; }
        public string? SubmitLabel { get; set; }
        #endregion
    }
}
