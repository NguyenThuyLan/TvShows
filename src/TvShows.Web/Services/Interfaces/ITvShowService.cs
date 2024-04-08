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

		IMedia ImportMediaFromTVMazeToUmbraco(TvShowModel tvShow);
	}
}
