using Examine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvShows.Web.Models;

namespace TvShows.Web.Services.Interfaces
{
	public interface ISearchService
	{
		IEnumerable<ISearchResult> GetSearchResults(string searchTerm, out long totalItemCount);
		IEnumerable<SearchResultItem> GetContentSearchResults(string searchTerm, out long totalItemCount);
	}
}
