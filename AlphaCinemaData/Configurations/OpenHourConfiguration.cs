using AlphaCinemaData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlphaCinemaData.Configurations
{
	public class OpenHourConfiguration : IEntityTypeConfiguration<OpenHour>
	{
		public void Configure(EntityTypeBuilder<OpenHour> builder)
		{
			builder
				.HasKey(oh => oh.Id);

			builder
				.Property(opHour => opHour.Hours)
				.IsRequired(true);
            
			builder
				.Property(opHour => opHour.Minutes)
				.IsRequired(true);

            builder
                .HasIndex(opHour => new { opHour.Hours, opHour.Minutes })
                .IsUnique(true);
            
			builder
				.HasMany(oh => oh.Projections)
				.WithOne(p => p.OpenHour)
				.HasForeignKey(p => p.OpenHourId);
		}
	}
}
