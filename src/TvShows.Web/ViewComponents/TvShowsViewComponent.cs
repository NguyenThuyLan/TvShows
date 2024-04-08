﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TvShows.Web.Models;
using TvShows.Web.Models.ViewComponentModels;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace TvShows.Web.ViewComponents
{
	[ViewComponent]
	public class TvShowsViewComponent : ViewComponent
	{
		private readonly ILogger<TvShowsViewComponent> _logger;
        private readonly IUmbracoContextFactory _umbracoContextFactory;
        private readonly IContentService _contentService;
        public TvShowsViewComponent(ILogger<TvShowsViewComponent> logger,
			IContentService contentService,
            IUmbracoContextFactory umbracoContextFactory)
        {
			_logger = logger;
            _umbracoContextFactory = umbracoContextFactory;
            _contentService = contentService;

        }

		public IViewComponentResult Invoke(int pageIndex = 1, int pageSize = 10)
		{
			PaginationModel paginationModel = new PaginationModel();
			
            try
            {
				var TvShows = new List<TvShowsView>();
				var parentId = _contentService.GetByLevel(2).ToList().Where(s => s.Name == "Home").First().Id;
				var contentParent = _umbracoContextFactory.EnsureUmbracoContext().UmbracoContext.Content.GetById(parentId) as TvShowsLibrary;
				var total = contentParent.Children<TvShow>().Count();
				var totalPages = (int)Math.Ceiling((double)total / (double)pageSize);
				var children = contentParent.Children<TvShow>().ToList().GetRange((pageIndex - 1) * pageSize, pageSize);
				if (children.Count > 0)
				{
					foreach (var child in children)
					{
						var tvshow = new TvShowsView()
						{
							Id = child.Id,
							Name = child.Name,
							Summary = child.Summary,
							Premiered = child.Premiered,
							Image = child.PreImage
						};
						TvShows.Add(tvshow);
					}
				}
				paginationModel.TotalPages = totalPages;
				paginationModel.TotalCount = total;
				paginationModel.Items = TvShows;
				paginationModel.CurrentPage = pageIndex;
			}
            catch (Exception ex)
            {
				_logger.LogError(ex, $"TvShowsViewComponent:Invoke:{ex.Message}");
            }
            

			return View(paginationModel);
		}
    }
}