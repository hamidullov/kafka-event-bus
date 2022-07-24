using Example.Cqrs;
using Example.IntegrationEvents.FlgStudies;
using Example.Mapper;
using FlgSeries.Microservice.Domain;
using FlgSeries.Microservice.Query.Models;

namespace FlgSeries.Microservice.Commands.AddStudy;

public class AddStudyCommand : ICommand, IMapFrom<FlgStudyPlacedIntegrationEvent>, IMapTo<Study>
{
    public StudyWithSeriesDto Study { get; set; }
}