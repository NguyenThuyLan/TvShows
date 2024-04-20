using Microsoft.EntityFrameworkCore;
using TvShows.Web.Models.Review;

namespace TvShows.Web.Common.Context
{
    public class TvShowContext : DbContext
    {
		public TvShowContext(DbContextOptions<TvShowContext> options) : base(options)
        {
		}

        public required DbSet<TvShowReview> TvShowReviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder.Entity<TvShowReview>(entity =>
            {
                entity.ToTable("tvShowReviews");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
				entity.Property(e => e.TvShowTitle).HasColumnName("tvShowTitle");
                entity.Property(e => e.UserName).HasColumnName("userName");
                entity.Property(e => e.Email).HasColumnName("email");
                entity.Property(e => e.Review).HasColumnName("review");
                entity.Property(e => e.IsApproved).HasColumnName("isApproved");
                entity.Property(e => e.CreatedDate).HasColumnType("datetime").HasColumnName("createdDate");
				entity.Property<Guid?>(e => e.TvShowKeyId).HasColumnType("uniqueidentifier").HasColumnName("tvShowKeyId");
				entity.Property<Guid?>(e => e.MemberKeyId).HasColumnType("uniqueidentifier").HasColumnName("memberKeyId");
			});
    }
}
