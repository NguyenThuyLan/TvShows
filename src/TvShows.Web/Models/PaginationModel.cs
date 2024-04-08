using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvShows.Web.Models.ViewComponentModels;

namespace TvShows.Web.Models
{
	public class PaginationModel
	{
		public int TotalCount { get; set; }
		public int TotalPages { get; set; }
		public int CurrentPage { get; set; }
		public IEnumerable<TvShowsView>? Items { get; set; }
	}
}
