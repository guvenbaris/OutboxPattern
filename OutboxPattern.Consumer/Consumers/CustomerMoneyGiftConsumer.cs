using MassTransit;
using Microsoft.EntityFrameworkCore;
using OutboxPattern.Infrastructure.Context;
using OutboxPattern.Shared.Events.Customers;

namespace OutboxPattern.Consumer.Consumers;

public sealed class CustomerMoneyGiftConsumer(ApplicationDbContext context) : IConsumer<CustomerMoneyGiftEvent>
{
    private static int GiftAmount = 10;
    private readonly ApplicationDbContext _context = context;

    public async Task Consume(ConsumeContext<CustomerMoneyGiftEvent> context)
    {
        if (context == null || context.Message == null)
            return;

        await _context.Customers
            .Where(x => x.Id == context.Message.CustomerId)
            .ExecuteUpdateAsync(setters => setters.SetProperty(b => b.Balance, b => b.Balance + GiftAmount));
    }
}
