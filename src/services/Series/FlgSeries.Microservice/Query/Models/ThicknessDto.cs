using Example.IntegrationEvents.FlgStudies.Models;
using Example.Mapper;
using FlgSeries.Microservice.Domain;

namespace FlgSeries.Microservice.Query.Models;

/// <summary>
/// Толщина среза в мм
/// </summary>
public class ThicknessDto :
    IMapTo<Thickness>, 
    IMapFrom<Thickness>, 
    IMapTo<ThicknessEventModel>,
    IMapFrom<ThicknessEventModel>
{
    /// <summary>
    /// Значение
    /// </summary>
    public int Value { get; set; }
}