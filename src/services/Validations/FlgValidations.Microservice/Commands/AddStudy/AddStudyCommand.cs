using AutoMapper;
using Example.Cqrs;
using Example.IntegrationEvents.FlgStudies;
using Example.Mapper;
using FlgValidations.Microservice.Commands.Models;

namespace FlgValidations.Microservice.Commands.AddStudy;

public class AddStudyCommand : ICommand, IMapFrom<FlgStudyPlacedIntegrationEvent>
{
    public StudyDto Study { get; set; }
}