using MediatR;
using OutboxPattern.Application.Interfaces.Repositories.Base;
using OutboxPattern.Application.Utilities.Results;
using System.Transactions;

namespace OutboxPattern.Application.Utilities.Behaviors;

public sealed class UnitOfWorkBehavior<TRequest, TResponse>(IUnitOfWork unitOfWork) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
    where TResponse : Result
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!typeof(TRequest).Name.EndsWith("Command"))
        {
            return await next();
        }

        var response = await next();

        await _unitOfWork.SaveChangesAsync();

        return response;              
    }
}
