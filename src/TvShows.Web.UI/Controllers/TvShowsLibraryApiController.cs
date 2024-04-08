using Microsoft.AspNetCore.Mvc;
using TvShows.Web.Models;
using TvShows.Web.Services.Interfaces;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Cms.Web.Common.PublishedModels;
using Umbraco.Cms.Web.Common.UmbracoContext;

namespace TvShows.Web.UI.Controllers
{
	public class TvShowsLibraryApiController : UmbracoApiController
	{
        private readonly ITvShowService _tvShowService;
        public TvShowsLibraryApiController(
            ITvShowService tvShowService)
		{
			_tvShowService = tvShowService;
		}

		[HttpGet]
		public void LoadDataFromTvMaze()
		{
			_tvShowService.MoveTvShowsFromTvMazeToUmbraco();
		}
	}
}
