using MediatR;

namespace Example.Cqrs;

public interface IQueryHandler<in TQueryContext, TResult>
    : IRequestHandler<TQueryContext, TResult>
    where TQueryContext : IQuery<TResult>
{

}