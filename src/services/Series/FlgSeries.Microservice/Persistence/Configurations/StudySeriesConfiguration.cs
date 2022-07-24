using FlgSeries.Microservice.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlgSeries.Microservice.Persistence.Configurations;

public class FlgStudySeriesConfiguration : IEntityTypeConfiguration<StudySeries>
{
    public void Configure(EntityTypeBuilder<StudySeries> builder)
    {
        builder.OwnsOne(x => x.Thickness);
        builder.HasMany(x => x.StudySeriesInstances)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
      
    }
}