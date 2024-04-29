using Microsoft.Extensions.Logging;
using System.Globalization;
using TvShows.Web.Models;
using TvShows.Web.Services.Interfaces;
using Umbraco.Forms.Core;
using Umbraco.Forms.Core.Enums;
using Umbraco.Forms.Core.Persistence.Dtos;

namespace TvShows.Web.Common.Workflow
{
	public class CreateTvShowWorkflow : WorkflowType
	{
		private readonly ILogger<CreateTvShowWorkflow> _logger;
		private readonly ITvShowService _tvShowService;

		public CreateTvShowWorkflow(ILogger<CreateTvShowWorkflow> logger,
			ITvShowService tvShowService)
		{
			_logger = logger;
			_tvShowService = tvShowService;

			this.Id = new Guid("ccbeb0d5-adaa-4729-8b4c-4bb439dc0202");
			this.Name = "CreateTvShowWorkflow";
			this.Description = "This workflow is for Creating new Tv Show";
			this.Icon = "icon-chat-active";
			this.Group = "Services";
		}
		public override Task<WorkflowExecutionStatus> ExecuteAsync(WorkflowExecutionContext context)
		{
			var currentCulture = CultureInfo.CurrentCulture.ToString();
			var tvShowModel = new ShowModel();
			tvShowModel.CreatedByForm = true;
			tvShowModel.TvShowGuidId = context.Record.UniqueId;
			// we can then iterate through the fields
			foreach (RecordField rf in context.Record.RecordFields.Values)
			{
				// and we can then do something with the collection of values on each field
				object val = rf.Values[0];

				switch (rf.Alias)
				{
					case "name":
						tvShowModel.ShowTitle = val.ToString();
						break;
					case "description":
						tvShowModel.Summary = val.ToString();
						break;
					case "premiered":
						tvShowModel.Premiered = (DateTime)val;
						break;
					case "preImage":
						tvShowModel.PreImage = val.ToString();
						break;

				}
			}
			_tvShowService.SaveTvShow(tvShowModel, currentCulture);

			return Task.FromResult(WorkflowExecutionStatus.Completed);
		}

		public override List<Exception> ValidateSettings()
		{
			return new List<Exception>();
		}
	}
}
