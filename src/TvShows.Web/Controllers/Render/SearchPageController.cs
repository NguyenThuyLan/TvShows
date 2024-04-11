using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using TvShows.Web.Models;
using TvShows.Web.Services.Interfaces;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;

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
            SearchModel searchPageModel = new SearchModel(CurrentPage, new PublishedValueFallback(_serviceContext, _variationContextAccessor));

			try
            {
				if (string.IsNullOrEmpty(query))
				{
					searchPageModel = new SearchModel(CurrentPage, new PublishedValueFallback(_serviceContext, _variationContextAccessor))
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
					searchPageModel = new SearchModel(CurrentPage, new PublishedValueFallback(_serviceContext, _variationContextAccessor))
					{
						Query = query,
						SearchResults = searchResults,
						TotalResults = totalItemCount
					};
					return CurrentTemplate(searchPageModel);
				}
			}
            catch (Exception ex)
            {
                _logger.LogError(ex, $"SearchPage: with query = {query}, message = {ex.Message}");
            }

			return CurrentTemplate(searchPageModel);
		}
    }
}
