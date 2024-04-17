
using Umbraco.UIBuilder.Configuration.Actions;

namespace TvShows.Web.Common.Actions
{
    public class EditTvShowReviewAction : Umbraco.UIBuilder.Configuration.Actions.Action<ActionResult>
    {
        public override string Icon => "icon-settings";
        public override string Alias => "editaction";
        public override string Name => "Edit";
        public override bool ConfirmAction => false;

        public EditTvShowReviewAction()
        {

        }

        public override ActionResult Execute(string collectionAlias, object[] entityIds)
        {
            // Perform operation here...
            return null;
        }
    }
}
