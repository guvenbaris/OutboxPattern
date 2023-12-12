using MediatR;
using OutboxPattern.Application.Utilities.Results;

namespace OutboxPattern.Application.Features.Orders.Create;

public sealed record OrderCreateCommand(string Description,
    IEnumerable<ProductQuantity> ProductQuantities) : IRequest<Result>;

public sealed record ProductQuantity(int Quantity,Guid ProductId);