using Example.IntegrationEvents.FlgStudies.Models;
using Example.Mapper;

namespace FlgStudies.Microservice.Queries.Models;

/// <summary>
/// Толщина среза в мм
/// </summary>
public class ThicknessDto : IMapTo<ThicknessEventModel>, IMapFrom<Domain.Thickness>
{
    /// <summary>
    /// Значение
    /// </summary>
    public int Value { get; set; }
}