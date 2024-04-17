using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TvShows.Web.Common.Context;
using TvShows.Web.Models;
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
			builder.Services.AddUmbracoDbContext<TvShowContext>((serviceProvider, options) =>
			{
				options.UseUmbracoDatabaseProvider(serviceProvider);
			});
		}
	}
}
