using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace TvShows.Web.Models
{
	public class SearchModel : PublishedContentWrapped
	{
		public SearchModel(IPublishedContent content, IPublishedValueFallback publishedValueFallback) : base(content, publishedValueFallback)
		{
		}
		public string Query { get; set; }
		public long TotalResults { get; set; }
		public IEnumerable<SearchResultItem> SearchResults { get; set; }
	}
}
