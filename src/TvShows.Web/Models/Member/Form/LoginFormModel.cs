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

        #region labelString
        public string LangCulture { get; set; }
		public string Title { get; set; }
		public string PasswordLabel { get; set; }
		public string EmailLabel {  get; set; }
		public string SubmitLabel { get; set; }
        #endregion
    }
}
