using FlgStudies.Microservice.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlgStudies.Microservice.Persistence.Configurations;

public class FlgStudiesConfiguration : IEntityTypeConfiguration<Study>
{
    public void Configure(EntityTypeBuilder<Study> builder)
    {
        builder.HasMany(x => x.Series)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(x => x.StateHistory)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.State)
            .HasConversion<string>();
    }
}