using Newtonsoft.Json;
using OutboxPattern.Domain.Entities.Base;
using OutboxPattern.Shared.Model;

namespace OutboxPattern.Shared.OutboxEvent;

public static class OutboxEventHandler
{
    public static Dictionary<Guid, OutboxMessage> OutboxEvents = [];

    public static void CreateOutboxEvent<TEntity>(string typeName,TEntity? entity = null, List<TEntity>? entities = null) where TEntity : BaseEntity
    {
        if (entity == null && entities == null)        
            return;

        if (entity != null)
        {
            var outboxMessage = new OutboxMessage
            {
                Id = Guid.NewGuid(),
                Content = JsonConvert.SerializeObject(entity),
                Type = typeName,
                CreatedDate = DateTime.UtcNow,
                CreatedEventId = entity.Id
            };

            if (!OutboxEvents.TryGetValue(entity.Id, out _))
            {
                OutboxEvents.TryAdd(entity.Id, outboxMessage);
            }
        }
        else
        {
            var entityFirst = entities!.First();

            var outboxMessage = new OutboxMessage
            {
                Id = Guid.NewGuid(),
                Content = JsonConvert.SerializeObject(entities),
                Type = typeName,
                CreatedDate = DateTime.UtcNow,
                CreatedEventId = entityFirst.Id
            };

            if (!OutboxEvents.TryGetValue(entityFirst.Id, out _))
            {
                OutboxEvents.TryAdd(entityFirst.Id, outboxMessage);
            }
        }        
    }

    public static void ClearOutboxEvents()
    {
        OutboxEvents.Clear();
    }
}
