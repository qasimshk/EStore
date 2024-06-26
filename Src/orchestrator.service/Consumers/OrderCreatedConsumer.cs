namespace orchestrator.service.Consumers;

using System.Threading.Tasks;
using estore.common.Events;
using estore.common.Models.Requests;
using MassTransit;
using orchestrator.service.Services;

public class OrderCreatedConsumer(IEStoreService eStoreService) : IConsumer<CreateOrderEvent>
{
    private readonly IEStoreService _eStoreService = eStoreService;

    public async Task Consume(ConsumeContext<CreateOrderEvent> context)
    {
        var response = await _eStoreService.CreateOrder(CreateOrderRequest.Map(context.Message));

        if (response.IsSuccess)
        {
            await context.Publish(new OrderCreatedSuccessfullyEvent
            {
                CorrelationId = context.Message.CorrelationId,
                OrderId = response.Value.OrderId,
            });
        }
        else
        {
            await context.Publish(new FailedEvent
            {
                CorrelationId = context.Message.CorrelationId,
                ConsumerName = nameof(OrderCreatedConsumer),
                ErrorMessage = response.ErrorMessage
            });
        }
    }
}
