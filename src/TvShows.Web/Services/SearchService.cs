using Examine;
using Microsoft.Extensions.Logging;
using System.Globalization;
using TvShows.Web.Models;
using TvShows.Web.Services.Interfaces;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Examine;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace TvShows.Web.Services
{
	public class SearchService : ISearchService
	{
		private readonly ILogger<SearchService> _logger;
		private readonly IExamineManager _examineManager;
		private readonly IUmbracoContextAccessor _umbracoContextAccessor;
		private readonly IProfiler _profiler;
        public SearchService(
			ILogger<SearchService> logger, 
			IExamineManager examineManager,
			IUmbracoContextAccessor umbracoContextAccessor,
			IProfiler profiler)
        {
			_examineManager = examineManager;
			_umbracoContextAccessor = umbracoContextAccessor;
			_logger = logger;
			_profiler = profiler;

		}
		public IEnumerable<SearchResultItem> GetContentSearchResults(string searchTerm, out long totalItemCount)
		{
			var pageOfResults = GetSearchResults(searchTerm, out totalItemCount);
			var items = new List<SearchResultItem>();
			if (pageOfResults != null && pageOfResults.Any())
			{
				using (_profiler.Step("Search logic"))
				{
					if (_umbracoContextAccessor.TryGetUmbracoContext(out var umbracoContext))
					{
						foreach (var item in pageOfResults)
						{
							var page = umbracoContext.Content.GetById(int.Parse(item.Id));
							if (page != null)
							{
								items.Add(new SearchResultItem()
								{
									PublishedItem = page as TvShow,
									Score = item.Score
								});
							}
						}
					}
				}
            }
			return items;
		}


		public IEnumerable<ISearchResult> GetSearchResults(string searchTerm, out long totalItemCount)
		{
			totalItemCount = 0;
			if (_examineManager.TryGetIndex(Constants.UmbracoIndexes.ExternalIndexName, out var index))
			{
				var searcher = index.Searcher;
				var fieldToSearch = "showTitle" + "_" + CultureInfo.CurrentCulture.ToString().ToLower();
				var criteria = searcher.CreateQuery(IndexTypes.Content);
				var examineQuery = criteria.Field(fieldToSearch, searchTerm.MultipleCharacterWildcard());
				var results = examineQuery.Execute();
				totalItemCount = results.TotalItemCount;
				if (results.Any())
				{
					return results;
				}
				else
				{
					_logger.LogWarning("Can not find any result.");
				}
			}
			return Enumerable.Empty<ISearchResult>();
		}

	}
}
