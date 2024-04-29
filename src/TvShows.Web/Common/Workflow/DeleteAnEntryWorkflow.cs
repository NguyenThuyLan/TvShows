using Umbraco.Forms.Core;
using Umbraco.Forms.Core.Enums;
using Umbraco.Forms.Core.Services;

namespace TvShows.Web.Common.Workflow
{
	public class DeleteAnEntryWorkflow : WorkflowType
	{
		private readonly IRecordService _recordService;
		public DeleteAnEntryWorkflow(IRecordService recordService)
		{
			_recordService = recordService;
			this.Id = new Guid("c6a2e7b3-8d1d-4e2a-9f7a-1e5d8f0e1b4a");
			this.Name = "Delete An Entry";
			this.Description = "This workflow is for deleting request show";
			this.Icon = "icon-delete";
			this.Group = "Services";
		}

		public override async Task<WorkflowExecutionStatus> ExecuteAsync(WorkflowExecutionContext context)
		{
			var record = context.Record;
			var form = context.Form;
			await _recordService.DeleteAsync(record, form);
			return await Task.FromResult(WorkflowExecutionStatus.Completed);
		}

		public override List<Exception> ValidateSettings()
		{
			return new List<Exception>();
		}
	}
}
