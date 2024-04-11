using Microsoft.Extensions.DependencyInjection;
using TvShows.Web.Services;
using TvShows.Web.Services.Interfaces;
using TvShows.Web.Utility;
using Umbraco.Cms.Core.Composing;

namespace TvShows.Web.Composer
{
	public class CommonComposer : IComposer
	{
		public void Compose(IUmbracoBuilder builder)
		{
			builder.Services.AddSingleton<ITvShowService, TvShowService>();
			builder.Services.AddSingleton<ISearchService, SearchService>();
			
		}
	}
}
