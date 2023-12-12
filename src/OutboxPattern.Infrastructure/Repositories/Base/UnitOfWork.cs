using OutboxPattern.Application.Interfaces.Repositories.Base;
using OutboxPattern.Infrastructure.Context;
using OutboxPattern.Shared.OutboxEvent;

namespace OutboxPattern.Infrastructure.Repositories.Base;

public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    private readonly ApplicationDbContext _context = context;

    public async Task SaveChangesAsync()
    {
        foreach (var entry in _context.ChangeTracker.Entries())
        {
            switch (entry.State)
            {
                // created, updated date, updated_by or created_by field can be modified in here.
                case Microsoft.EntityFrameworkCore.EntityState.Modified:
                    break;
                case Microsoft.EntityFrameworkCore.EntityState.Added:
                    break;
                default:
                    break;
            }
        }

        if (OutboxEventHandler.OutboxEvents.Count != 0)
        {
            foreach (var outboxEvent in OutboxEventHandler.OutboxEvents)
            {
                await _context.OutboxMessages.AddAsync(outboxEvent.Value);
            }

            OutboxEventHandler.ClearOutboxEvents();
        }

        await _context.SaveChangesAsync();
    }
}
