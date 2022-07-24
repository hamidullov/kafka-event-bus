using Example.DDD;

namespace FlgValidations.Microservice.Domain;

/// <summary>
/// Толщина среза в мм
/// </summary>
public class Thickness : ValueObject
{
    public Thickness(int value)
    {
        Value = value;
    }

    /// <summary>
    /// Значение
    /// </summary>
    public int Value { get; set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}