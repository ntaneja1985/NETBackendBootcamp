

using Basket.Basket.Features.UpdateItemPriceInBasket;


namespace Basket.Basket.EventHandlers
{
    public class ProductPriceChangedIntegrationEventHandler(ISender sender, ILogger<ProductPriceChangedIntegrationEventHandler> logger) : IConsumer<ProductPriceChangedIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<ProductPriceChangedIntegrationEvent> context)
        {
            
           logger.LogInformation("Integration Event handled: {IntegrationEvent}",context.Message.GetType().Name);
            //Find basket items with Product Id and update Item Price
            
            //mediatr new command and handler to find products on basket and update the price.
            var result = await sender.Send(new UpdateItemPriceInBasketCommand(context.Message.ProductId,context.Message.Price));
            if(!result.IsSuccess)
            {
                logger.LogError("Error updating price in basket for productId: {ProductId}", context.Message.ProductId);
            }
            logger.LogInformation($"Price for the product Id: {context.Message.ProductId} updated in basket");
        }
    }
}
