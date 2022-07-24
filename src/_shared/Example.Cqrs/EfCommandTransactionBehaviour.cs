using Example.EfCore;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Example.Cqrs;

public class EfCommandTransactionBehaviour<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly BaseAuditableDbContext _dbContext;
    private readonly ILogger<EfCommandTransactionBehaviour<TRequest, TResponse>> _logger;

    public EfCommandTransactionBehaviour(
        BaseAuditableDbContext dbContext,
        ILogger<EfCommandTransactionBehaviour<TRequest, TResponse>> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        var requestName = request.GetType().Name;
        _logger.LogInformation("Start request {RequestName}", requestName);
        if (!IsCommand(request)) return await next();

        await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            var response = await next();
            await _dbContext.SaveChangesAsync(cancellationToken);
            await _dbContext.Database.CommitTransactionAsync(cancellationToken);
            OnSuccess(response);
            await OnSuccessAsync(response, cancellationToken);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"EfCommandTransactionBehaviour: {ex.Message}");
            await _dbContext.Database.RollbackTransactionAsync(cancellationToken);
            throw;
        }
        finally
        {
            _logger.LogInformation("End request {RequestName}", requestName);
        }
    }

    private async Task OnSuccessAsync(TResponse response, CancellationToken cancellationToken)
    {
        var responseType = response.GetType();
        if (responseType.GetInterfaces()
            .All(x => !x.IsGenericType || x.GetGenericTypeDefinition() != typeof(IPromiseAsync<>))) return;
        switch (response)
        {
            case IPromiseAsync<int> promiseInt:
                await promiseInt.OnSuccessAsync(cancellationToken);
                break;
            case IPromiseAsync<Unit> promiseUnit:
                await promiseUnit.OnSuccessAsync(cancellationToken);
                break;
            default:
            {
                var method = responseType.GetMethod("OnSuccessAsync");
                dynamic awaitable = @method.Invoke(response, new object?[] { cancellationToken });
                await awaitable;
                break;
            }
        }
    }

    private static void OnSuccess(TResponse response)
    {
        var responseType = response.GetType();
        if (responseType.GetInterfaces()
            .All(x => !x.IsGenericType || x.GetGenericTypeDefinition() != typeof(IPromise<>))) return;
        switch (response)
        {
            case IPromise<int> promiseInt:
                promiseInt.OnSuccess();
                break;
            case IPromise<Unit> promiseUnit:
                promiseUnit.OnSuccess();
                break;
            default:
            {
                var method = responseType.GetMethod("OnSuccess");
                method.Invoke(response, Array.Empty<object>());
                break;
            }
        }
    }

    private static bool IsCommand(TRequest request)
    {
        var interfaces = request.GetType().GetInterfaces();
        return interfaces.Any(x =>
            x == typeof(ICommand) || x.IsGenericType && x.GetGenericTypeDefinition() == typeof(ICommand<>));
    }
}