using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Forms.Core.Persistence.Dtos;

namespace TvShows.Web.Models
{
    public class RequestShowModel : PublishedContentWrapped
    {
        public RequestShowModel(
            IPublishedContent content,
            IPublishedValueFallback publishedValueFallback)
            : base(content, publishedValueFallback)
        {
        }

        public long TotalResults { get; set; }

        public IEnumerable<Record> Records { get; set; }
    }
}
