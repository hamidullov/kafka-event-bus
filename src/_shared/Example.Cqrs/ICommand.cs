using MediatR;

namespace Example.Cqrs;

public interface ICommand : IRequest
{

}
    
public interface ICommand<T> : IRequest<T>
{

}