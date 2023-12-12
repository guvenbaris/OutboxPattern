using MassTransit;
using Newtonsoft.Json;
using OutboxPattern.Domain.Entities;
using OutboxPattern.Shared.Constants;
using OutboxPattern.Shared.Events.Customers;
using OutboxPattern.Shared.Events.Orders;
using OutboxPattern.Shared.Model;
using Quartz;

namespace OutboxPattern.Pulling;
public class OutboxPublishJob : IJob
{
    private readonly IPublishEndpoint _publishEndpoint;

    public OutboxPublishJob(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        if (DapperSingletonDatabase.DataReaderState)
        {
            DapperSingletonDatabase.DataReaderBusy();

            List<OutboxMessage> outboxMessages = (await DapperSingletonDatabase
                .QueryAsync<OutboxMessage>($@"SELECT * FROM outbox_message WHERE processed_date IS NULL ORDER By created_date DESC")).ToList();

            foreach (OutboxMessage outboxMessage in outboxMessages)
            {
                bool published = false;
                if (outboxMessage.Type == EventConstant.OrderQuantityControlEvent)
                {
                    var orderProductEntities = JsonConvert.DeserializeObject<List<OrderProductEntity>>(outboxMessage.Content);

                    if (orderProductEntities != null)
                    {    
                        await _publishEndpoint.Publish(new OrderQuantityControlEvent(
                                orderProductEntities.First().OrderId,
                                orderProductEntities.Select(x => new ProductQuantity(x.ProductId, x.Quantity)).ToList()));

                        published = true;
                    }
                }
                else if (outboxMessage.Type == EventConstant.CustomerMoneyGiftEvent)
                {
                    var customer = JsonConvert.DeserializeObject<CustomerEntity>(outboxMessage.Content);

                    if (customer != null && customer.Email != null)
                    {
                        await _publishEndpoint.Publish(new CustomerMoneyGiftEvent(customer.Id, customer.Email));

                        published = true;
                    }
                }

                if (published)
                {
                    await DapperSingletonDatabase.ExecuteAsync(@$"UPDATE outbox_message SET processed_date = '{DateTime.UtcNow}'
                                                                          WHERE created_event_id = '{outboxMessage.CreatedEventId}'");
                }
            }

            DapperSingletonDatabase.DataReaderReady();
        }
    }
}

