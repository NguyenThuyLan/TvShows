using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using TvShows.Web.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Forms.Core.Services;

namespace TvShows.Web.Controllers
{
    public class PagingPageController : RenderController
    {

        private readonly ILogger<PagingPageController> _logger;
        private readonly ServiceContext _serviceContext;
        private readonly IVariationContextAccessor _variationContextAccessor;
        private readonly IRecordReaderService _recordReaderService;
		public PagingPageController(
            ILogger<PagingPageController> logger,
            ICompositeViewEngine compositeViewEngine,
            IUmbracoContextAccessor umbracoContextAccessor,
            ServiceContext services,
            IVariationContextAccessor variationContextAccessor,
			IRecordReaderService recordReaderService) 
            : base(logger, compositeViewEngine, umbracoContextAccessor)
        {
            _logger = logger;
            _serviceContext = services;
            _variationContextAccessor = variationContextAccessor;
			_recordReaderService = recordReaderService;

		}

        public IActionResult PagingPage()
        {
            RequestShowModel requestShowModel = new RequestShowModel(CurrentPage, new PublishedValueFallback(_serviceContext, _variationContextAccessor));
            var contentType = CurrentPage.GetProperty("contentTableType").GetValue();
			return CurrentTemplate(requestShowModel);
        }
    }
}
