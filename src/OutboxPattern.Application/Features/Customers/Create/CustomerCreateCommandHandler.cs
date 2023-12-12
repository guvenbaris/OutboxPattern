using MediatR;
using OutboxPattern.Application.Interfaces.Repositories;
using OutboxPattern.Application.Utilities.Results;
using OutboxPattern.Domain.Entities;
using OutboxPattern.Shared.Constants;
using OutboxPattern.Shared.OutboxEvent;

namespace OutboxPattern.Application.Features.Customers.Create;

public sealed class CustomerCreateCommandHandler(ICustomerRepository customerRepository) : IRequestHandler<CustomerCreateCommand, Result>
{
    private readonly ICustomerRepository _customerRepository = customerRepository;

    public async Task<Result> Handle(CustomerCreateCommand request, CancellationToken cancellationToken)
    {
        var customer = new CustomerEntity
        {
            Email = request.Email,
            Name = request.Name,
            Surname = request.Surname
        };

        await _customerRepository.AddAsync(customer);

        OutboxEventHandler.CreateOutboxEvent(EventConstant.CustomerMoneyGiftEvent,customer);

        return await Result.Success();
    }
}
