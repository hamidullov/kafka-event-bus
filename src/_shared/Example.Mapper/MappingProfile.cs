using System.Reflection;
using AutoMapper;
using Type = System.Type;

namespace Example.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        ApplyMappingsFromAssembly(Assembly.GetEntryAssembly());
        ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
    }

    private void ApplyMappingsFromAssembly(Assembly assembly)
    {
        var typesFrom = assembly.GetExportedTypes()
            .Where(t => t.GetInterfaces().Any(i => 
                i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
            .ToList();
            
        var typesTo = assembly.GetExportedTypes()
            .Where(t => t.GetInterfaces().Any(i => 
                i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapTo<>)))
            .ToList();

        InvokeTypes(typesFrom, "IMapFrom`1");
        InvokeTypes(typesTo, "IMapTo`1");
    }

    private void InvokeTypes(List<Type> types, string typeName)
    {
        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);
            var methodInfo = type.GetMethod("Mapping");
            if (methodInfo != null)
            {
                methodInfo?.Invoke(instance, new object[] { this });
            }
            else
            {
                var interfaces = type.GetInterfaces().Where(r=> r.FullName.Contains(typeName));
                foreach (var @interface in interfaces)
                {
                    methodInfo = @interface.GetMethod("Mapping");
                    methodInfo?.Invoke(instance, new object[] { this });
                }
            }
        }
    }
}