using MediatR;
using OutboxPattern.Application.Utilities.Results;

namespace OutboxPattern.Application.Features.Customers.Create;

public sealed record class CustomerCreateCommand(
    string Name,
    string Surname,
    string Email) : IRequest<Result>;
