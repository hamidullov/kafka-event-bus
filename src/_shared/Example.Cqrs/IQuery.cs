using MediatR;

namespace Example.Cqrs;

public interface IQuery<out TResult> : IRequest<TResult>
{

}