using Microsoft.Extensions.Logging;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
			_tvShowService= tvShowService;

			this.Id = new Guid("ccbeb0d5-adaa-4729-8b4c-4bb439dc0202");
			this.Name = "CreateTvShowWorkflow";
			this.Description = "This workflow is for Creating new Tv Show";
			this.Icon = "icon-chat-active";
			this.Group = "Services";
		}
		public override Task<WorkflowExecutionStatus> ExecuteAsync(WorkflowExecutionContext context)
		{
			var tvShowModel = new TvShowModel();
			// we can then iterate through the fields
			foreach (RecordField rf in context.Record.RecordFields.Values)
			{
				// and we can then do something with the collection of values on each field
				object val = rf.Values[0];

				switch (rf.Alias)
				{
					case "name":
						tvShowModel.Name = val.ToString();
						break;
					case "description":
						tvShowModel.Summary = val.ToString();
						break;
					case "premiered":
						tvShowModel.Premiered = (DateTime)val;
						break;

				}
			}
			_tvShowService.InsertedOrUpdated(tvShowModel);

			return Task.FromResult(WorkflowExecutionStatus.Completed);
		}

		public override List<Exception> ValidateSettings()
		{
			return new List<Exception>();
		}
	}
}
