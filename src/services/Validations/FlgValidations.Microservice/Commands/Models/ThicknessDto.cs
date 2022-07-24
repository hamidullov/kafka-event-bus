using Example.IntegrationEvents;
using Example.IntegrationEvents.FlgStudies;
using Example.IntegrationEvents.FlgStudies.Models;
using Example.Mapper;

namespace FlgValidations.Microservice.Commands.Models;

/// <summary>
/// Толщина среза в мм
/// </summary>
public class ThicknessDto : IMapFrom<ThicknessEventModel>, IMapTo<Domain.Thickness>
{
    public ThicknessDto(){}
    public ThicknessDto(int value)
    {
        Value = value;
    }

    /// <summary>
    /// Значение
    /// </summary>
    public int Value { get; set; }
}