using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace TvShows.Web.Models
{
	public class SearchResultItem
	{
        public TvShow PublishedItem { get; init; }
		public float Score { get; init; }
	}
}
