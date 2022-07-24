namespace Example.Common.Exceptions
{
    public class AppNotFoundException : AppException
    {
        public AppNotFoundException(string entityName, object id) : base($"Не удалось найти [{entityName}] с ид [{id}]")
        {
        }
    }
}