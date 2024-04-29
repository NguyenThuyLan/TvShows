using TvShows.Web.Models;
using Umbraco.Cms.Core.Models;

namespace TvShows.Web.Services.Interfaces
{
	public interface ITvShowService
	{
		string MoveTvShowsFromTvMazeToUmbraco();
		void DeleteAllTvShows();
		IMedia ImportMediaFromTVMazeToUmbraco(TvShowModel tvShow);
		bool InsertedOrUpdated(TvShowModel show);
		bool SaveTvShow(ShowModel show, string currentCulture);
		bool DeleteTvShow(Guid Id);
	}
}
