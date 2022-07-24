#nullable enable
namespace Example.Common.Exceptions
{
    public class InvalidStateDomainException : Exception
    {
        public InvalidStateDomainException(Enum currentState, Enum needState)
            : base($"Невалидное состояние объекта {currentState}. Требуемое состояние {needState}")
        {
        }
    }
}