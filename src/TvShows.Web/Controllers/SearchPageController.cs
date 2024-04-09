using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using Polly;
using TvShows.Web.Models;
using TvShows.Web.Services;
using TvShows.Web.Services.Interfaces;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Cms.Web.Common.PublishedModels;
using Umbraco.Cms.Web.Website.Controllers;

namespace TvShows.Web.Controllers
{
    public class SearchPageController : RenderController
    {
        private readonly ILogger<SearchPageController> _logger;
        private readonly ServiceContext _serviceContext;
        private readonly IVariationContextAccessor _variationContextAccessor;
        private readonly ISearchService _searchService;
        public SearchPageController(
            ILogger<SearchPageController> logger,
            ICompositeViewEngine compositeViewEngine,
            IUmbracoContextAccessor umbracoContextAccessor,
            ServiceContext services,
            IVariationContextAccessor variationContextAccessor,
			ISearchService searchService) : base(logger, compositeViewEngine, umbracoContextAccessor)
        {
            _logger = logger;
            _serviceContext = services;
            _variationContextAccessor = variationContextAccessor;
			_searchService = searchService;

		}

        public IActionResult SearchPage(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                var searchPageModel = new SearchModel(CurrentPage, new PublishedValueFallback(_serviceContext, _variationContextAccessor))
                {
                    Query = "_",
                    SearchResults = Enumerable.Empty<SearchResultItem>(),
                    TotalResults = 0
                };
                return CurrentTemplate(searchPageModel);
			}
			else
            {
				var searchResults = _searchService.GetContentSearchResults(query, out var totalItemCount);
				var searchPageModel = new SearchModel(CurrentPage, new PublishedValueFallback(_serviceContext, _variationContextAccessor))
				{
					Query = query,
					SearchResults = searchResults,
					TotalResults = totalItemCount
				};
				return CurrentTemplate(searchPageModel);
			}
        }
    }
}
