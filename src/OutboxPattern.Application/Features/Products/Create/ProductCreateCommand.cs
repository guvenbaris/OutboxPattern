using MediatR;
using OutboxPattern.Application.Utilities.Results;

namespace OutboxPattern.Application.Features.Products.Create;
public sealed record class ProductCreateCommand(
    string Name,
    decimal Price,
    int StockAmount,
    string Description) : IRequest<Result>;
