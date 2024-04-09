using Examine;
using Examine.Search;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvShows.Web.Models;
using TvShows.Web.Services.Interfaces;
using Umbraco.Cms.Core;
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
        public SearchService(ILogger<SearchService> logger, IExamineManager examineManager, IUmbracoContextAccessor umbracoContextAccessor)
        {
			_examineManager = examineManager;
			_umbracoContextAccessor = umbracoContextAccessor;
			_logger = logger;

		}
		public IEnumerable<SearchResultItem> GetContentSearchResults(string searchTerm, out long totalItemCount)
		{
			var pageOfResults = GetSearchResults(searchTerm, out totalItemCount);
			var items = new List<SearchResultItem>();
			if (pageOfResults != null && pageOfResults.Any())
			{
				foreach (var item in pageOfResults)
				{
					if (_umbracoContextAccessor.TryGetUmbracoContext(out var umbracoContext))
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
			return items;
		}


		public IEnumerable<ISearchResult> GetSearchResults(string searchTerm, out long totalItemCount)
		{
			totalItemCount = 0;
			if (_examineManager.TryGetIndex(Constants.UmbracoIndexes.ExternalIndexName, out var index))
			{
				var searcher = index.Searcher;
				var fieldToSearch = "showTitle";// + "_" + CultureInfo.CurrentCulture.ToString().ToLower();
				var hideFromNavigation = "umbracoNaviHide";
				var criteria = searcher.CreateQuery(IndexTypes.Content);
				var examineQuery = criteria.Field(fieldToSearch, searchTerm.MultipleCharacterWildcard());
				examineQuery.Not().Field(hideFromNavigation, 1.ToString());
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
