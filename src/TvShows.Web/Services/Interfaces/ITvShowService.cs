using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvShows.Web.Models;
using Umbraco.Cms.Core.Models;

namespace TvShows.Web.Services.Interfaces
{
	public interface ITvShowService
	{
		string MoveTvShowsFromTvMazeToUmbraco();
		void DeleteTvShows();

		IMedia ImportMediaFromTVMazeToUmbraco(TvShowModel tvShow);

		bool InsertedOrUpdated(TvShowModel show);
	}
}
