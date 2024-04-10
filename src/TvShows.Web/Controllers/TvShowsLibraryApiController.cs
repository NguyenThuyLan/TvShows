using Microsoft.AspNetCore.Mvc;
using TvShows.Web.Services.Interfaces;
using Umbraco.Cms.Web.Common.Controllers;

namespace TvShows.Web.Controllers
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

		[HttpDelete]
		public void DeleteTvShows()
		{
			_tvShowService.DeleteTvShows();
		}
	}
}
