namespace Example.IntegrationEvents.FlgStudies.Models;

/// <summary>
/// Толщина среза в мм
/// </summary>
public class ThicknessEventModel
{
    public ThicknessEventModel(int value)
    {
        Value = value;
    }

    /// <summary>
    /// Значение
    /// </summary>
    public int Value { get; set; }
}