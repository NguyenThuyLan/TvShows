using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models;

namespace TvShows.Web.Models.ViewComponentModels
{
	public class TvShowsView
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? Summary { get; set; }
		public DateTime? Premiered { get; set; }
        public string Url { get; set; }
        public MediaWithCrops? Image { get; set; }
	}
}
