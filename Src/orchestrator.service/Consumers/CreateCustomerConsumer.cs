namespace orchestrator.service.Consumers;

using System.Threading.Tasks;
using estore.common.Events;
using estore.common.Models.Requests;
using MassTransit;
using orchestrator.service.Services;

public class CreateCustomerConsumer(IEStoreService eStoreService) : IConsumer<CreateCustomerEvent>
{
    private readonly IEStoreService _eStoreService = eStoreService;

    public async Task Consume(ConsumeContext<CreateCustomerEvent> context)
    {
        var result = await _eStoreService.CreateCustomer(CreateCustomerRequest.Map(context.Message));

        if (result.IsSuccess)
        {
            await context.Publish(new CustomerCreatedSuccessfullyEvent
            {
                CorrelationId = context.Message.CorrelationId,
                CustomerId = result.Value.CustomerID
            });
        }
        else
        {
            await context.Publish(new FailedEvent
            {
                CorrelationId = context.Message.CorrelationId,
                ConsumerName = nameof(CreateCustomerConsumer),
                ErrorMessage = result.ErrorMessage
            });
        }
    }
}
