using MediatR;

namespace Example.Cqrs;

public interface ICommandHandler<in TCommandContext>
    : IRequestHandler<TCommandContext>
    where TCommandContext : ICommand
{

}
    
public interface ICommandHandler<in TCommandContext, TResult>
    : IRequestHandler<TCommandContext, TResult>
    where TCommandContext : ICommand<TResult>
{

}