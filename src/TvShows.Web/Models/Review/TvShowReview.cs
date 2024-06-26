﻿using NPoco;
using System.ComponentModel.DataAnnotations.Schema;
using Umbraco.Cms.Infrastructure.Persistence.DatabaseAnnotations;

namespace TvShows.Web.Models.Review
{
	[TableName("tvShowReviews")]
	[PrimaryKey("Id")]
	public sealed class TvShowReview
	{
		[PrimaryKeyColumn]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string TvShowTitle { get; set; } = string.Empty;
		public required string UserName { get; set; }
		public required string Email { get; set; }
		public string Review { get; set; } = string.Empty;
		public bool IsApproved { get; set; } = false;
		public DateTime CreatedDate { get; set; } = DateTime.Now;
		public Guid? TvShowKeyId { get; set; }
		public Guid? MemberKeyId { get; set; }
	}
}
