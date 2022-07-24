using FlgStudies.Microservice.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlgStudies.Microservice.Persistence.Configurations;

public class StudyStateHistoriesConfiguration : IEntityTypeConfiguration<StudyStateHistory>
{
    public void Configure(EntityTypeBuilder<StudyStateHistory> builder)
    {
        builder.Property(x => x.State)
            .HasConversion<string>();
    }
}