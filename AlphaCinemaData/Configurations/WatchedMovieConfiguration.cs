using AlphaCinemaData.Models.Associative;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlphaCinemaData.Configurations
{
    public class WatchedMovieConfiguration : IEntityTypeConfiguration<WatchedMovie>
    {
        public void Configure(EntityTypeBuilder<WatchedMovie> builder)
        {
			builder
				.HasKey(wm => wm.Id);

			builder
				.Property(wm => wm.UserId)
				.IsRequired(true);

			builder
                .HasIndex(wm => new
                {
                    wm.UserId,
                    wm.ProjectionId,
					wm.Date
                })
				.IsUnique(true);

			//builder
			//	.HasOne(wm => wm.User)
			//	.WithMany(u => u.WatchedMovies)
			//	.HasForeignKey(wm => new { Id = wm.Id);

			builder
				.HasOne(p => p.Projection)
				.WithMany(pr => pr.WatchedMovies)
				.HasForeignKey(p => p.ProjectionId);
		}
    }
}
