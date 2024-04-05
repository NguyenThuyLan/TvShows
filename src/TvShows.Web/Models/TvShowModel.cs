using System.Text.Json.Serialization;

namespace TvShows.Web.Models
{
	public class TvShowModel
	{
		[JsonPropertyName("id")] public int Id { get; set; }
		[JsonPropertyName("name")] public string? Name { get; set; }
		[JsonPropertyName("image")] public Image? Image { get; set; }
	}

	public class Image
	{
		[JsonPropertyName("medium")] public string? Medium { get; set; }
		[JsonPropertyName("original")] public string? Original { get; set; }
	}
}
