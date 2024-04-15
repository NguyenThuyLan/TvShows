using Microsoft.AspNetCore.Mvc;
using TvShows.Web.Models.Member.Form;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Security;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Common.Security;
using Umbraco.Cms.Web.Website.Controllers;
using Umbraco.Cms.Web.Website.Models;

namespace TvShows.Web.Controllers
{
	public class LoginController : SurfaceController
	{
		private readonly IMemberManager _memberManager;
		private readonly MemberModelBuilderFactory _memberModelBuilderFactory;
		private readonly IMemberSignInManager _signInManager;
		public LoginController(
			IUmbracoContextAccessor umbracoContextAccessor,
			IUmbracoDatabaseFactory databaseFactory,
			ServiceContext services,
			AppCaches appCaches,
			IProfilingLogger profilingLogger, 
			IPublishedUrlProvider publishedUrlProvider,
			IMemberManager memberManager,
			MemberModelBuilderFactory memberModelBuilderFactory,
			IMemberSignInManager signInManager) 
			: base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
		{
			_memberManager = memberManager;
			_memberModelBuilderFactory = memberModelBuilderFactory;
			_signInManager = signInManager;
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Submit(LoginFormModel model)
		{
			if (!ModelState.IsValid) return CurrentUmbracoPage();

			var profileModel = await _memberModelBuilderFactory.CreateProfileModel().BuildForCurrentMemberAsync();
			var result = await _signInManager.PasswordSignInAsync(model.StoreUserName, model.Password, false, true);

			if (!result.Succeeded)
			{
				ModelState.AddModelError("error", "Unable to authenticate with the provided credentials.");
				return CurrentUmbracoPage();
			}

			var signInResult = await _signInManager.PasswordSignInAsync(model.StoreUserName, model.Password, false, true);

			if (!signInResult.Succeeded)
			{
				ModelState.AddModelError("error", "Unable to authenticate with the provided credentials.");
				return CurrentUmbracoPage();
			}

			return Redirect("/");
		}

		[HttpGet]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();

			return Redirect("/");
		}

	}
}
