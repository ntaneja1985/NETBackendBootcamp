using MassTransit;
using Shared.Messaging.Events;

namespace Catalog.Products.EventHandlers
{
    public class ProductPriceChangedEventHandler(ILogger<ProductPriceChangedEventHandler> logger, IBus bus) : INotificationHandler<ProductPriceChangedEvent>
    {
        public async Task Handle(ProductPriceChangedEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Domain Event handled: {DomainEvent}", notification.GetType().Name);
            //Publish Product Price Changed Integration Event for Update Basket Prices
            var integrationEvent = new ProductPriceChangedIntegrationEvent
            {
                ProductId = notification.product.Id,
                Name = notification.product.Name,
                Price = notification.product.Price,
                ImageFile = notification.product.ImageFile,
                Category = notification.product.Category,
                Description = notification.product.Description

            };

            await bus.Publish(integrationEvent);
        }
    }
}
