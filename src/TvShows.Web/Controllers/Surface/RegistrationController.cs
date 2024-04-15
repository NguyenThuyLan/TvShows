

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TvShows.Web.Models.Member.Form;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Security;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Infrastructure.Scoping;
using Umbraco.Cms.Web.Common.Security;
using Umbraco.Cms.Web.Website.Controllers;

namespace TvShows.Web.Controllers
{
	public class RegistrationController : SurfaceController
    {
        private readonly IMemberSignInManager _memberSignInManager;
        private readonly IMemberService _memberService;
        private readonly IMemberManager _memberManager;
        private readonly IScopeProvider _scopeProvider;
        private readonly ILocalizationService _localizationService;

        public RegistrationController(
            IUmbracoContextAccessor umbracoContextAccessor,
            IUmbracoDatabaseFactory databaseFactory,
            ServiceContext services,
            AppCaches appCaches,
            IProfilingLogger profilingLogger,
            IPublishedUrlProvider publishedUrlProvider,
            IMemberSignInManager memberSignInManager,
            IMemberService memberService,
            IMemberManager memberManager,
            IScopeProvider scopeProvider,
            ILocalizationService localizationService)
            : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
        {
            _memberSignInManager = memberSignInManager;
            _memberService = memberService;
            _memberManager = memberManager;
            _scopeProvider = scopeProvider;
            _localizationService = localizationService; 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit(RegistrationFormModel model)
        {
            if (!ModelState.IsValid)
            {
                //var langugages = _localizationService.GetAllLanguages();
                //foreach (var key in ModelState.Keys)
                //{
                //    var errors = ModelState[key].Errors;
                //    if (errors != null && errors.Any())
                //    {
                //        ModelState[key].Errors.Clear();
                //        var errorMessage = $"this is custom message";
                //        ModelState[key].Errors.Add(errorMessage);
                //    }
                //    else
                //    {
                //        ModelState[key].Errors.Clear();
                //    }
                //}
                return CurrentUmbracoPage();
            } 
                
            var existing = _memberService.GetByEmail(model.Email);
            if (existing != null)
            {
                ModelState.AddModelError("exists", "Looks like you've already created an account, friend! :-)");
                return CurrentUmbracoPage();
            }

            var result = await RegisterMemberAsync(model, true); 

			if (!result.Succeeded)
            {
                ModelState.AddModelError("exists", "Could not login: " + result);
            }

            return Redirect("/");
        }

        private async Task<IdentityResult> RegisterMemberAsync(RegistrationFormModel model, bool logMemberIn)
        {
            using IScope scope = _scopeProvider.CreateScope(autoComplete: true);

            var identityUser = MemberIdentityUser.CreateNew(model.StoreUserName, model.Email, "storeMember", true);

            IdentityResult identityResult = await _memberManager.CreateAsync(
                identityUser,
                model.Password);

            if (identityResult.Succeeded)
            {
                // Update the custom properties
                // TODO: See TODO in MembersIdentityUser, Should we support custom member properties for persistence/retrieval?
                IMember member = _memberService.GetByKey(identityUser.Key);
                if (member == null)
                {
                    // should never happen
                    throw new InvalidOperationException($"Could not find a member with key: {member.Key}.");
                }
                member.SetValue("storeUserName", model.StoreUserName);

                _memberService.Save(member);

                if (logMemberIn)
                {
                    await _memberSignInManager.SignInAsync(identityUser, false);
                }
            }

            return identityResult;
        }
    }
}
