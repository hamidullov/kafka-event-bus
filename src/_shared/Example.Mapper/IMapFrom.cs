using AutoMapper;

namespace Example.Mapper;

public interface IMapFrom<T>
    // where T: struct, Enum
{   
    void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
}