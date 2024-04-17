using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TvShows.Web.Models.Review
{
    public sealed class TvShowReview
    {
        public int Id { get; set; }
        public Guid? TvShowKey { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string Website { get; set; }
        public string Review { get; set; } = string.Empty;
    }
}
