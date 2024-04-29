using Microsoft.Extensions.DependencyInjection;
using TvShows.Web.Common.Context;
using TvShows.Web.Common.Workflow;
using TvShows.Web.Services;
using TvShows.Web.Services.Interfaces;
using TvShows.Web.Utility;
using Umbraco.Cms.Core.Composing;
using Umbraco.Forms.Core.Providers;

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
			builder.WithCollectionBuilder<WorkflowCollectionBuilder>()
				.Add<CreateTvShowWorkflow>()
				.Add<DeleteAnEntryWorkflow>();
				
		}
	}
}
