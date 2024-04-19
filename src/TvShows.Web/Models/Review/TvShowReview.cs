using NPoco;
using Umbraco.Cms.Infrastructure.Persistence.DatabaseAnnotations;

namespace TvShows.Web.Models.Review
{
    [TableName("tvShowReviews")]
    [PrimaryKey("Id")]
    public sealed class TvShowReview
    {
        [PrimaryKeyColumn]
        public int Id { get; set; }
        public Guid? TvShowKey { get; set; }
        public string TvShowTitle { get; set; } = string.Empty;
		public Guid? MemberId { get; set; }
		public required string UserName { get; set; }
        public required string Email { get; set; }
        public string Review { get; set; } = string.Empty;
        public bool IsApproved { get; set; } = false;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
