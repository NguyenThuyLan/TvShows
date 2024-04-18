using Umbraco.Cms.Web.Common.PublishedModels;

namespace TvShows.Web.Models
{
	public class SearchResultItem
	{
        public TvShow PublishedItem { get; init; }
		public float Score { get; init; }
	}
}
