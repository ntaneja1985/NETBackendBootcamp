


using Microsoft.Extensions.Caching.Distributed;

namespace Basket.Basket.Features.UpdateItemPriceInBasket
{
    public record UpdateItemPriceInBasketCommand(Guid ProductId, decimal Price): ICommand<UpdateItemPriceInBasketResult>;

    public record UpdateItemPriceInBasketResult(bool IsSuccess);

    public class UpdateItemPriceInBasketCommandValidator: AbstractValidator<UpdateItemPriceInBasketCommand>
    {
        public UpdateItemPriceInBasketCommandValidator()
        {
            RuleFor(x=>x.ProductId).NotEmpty().WithMessage("Product Id is required");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than zero");
        }
    }

    public class UpdateItemPriceInBasketHandler(BasketDbContext basketDbContext) : ICommandHandler<UpdateItemPriceInBasketCommand, UpdateItemPriceInBasketResult>
    {
        public async Task<UpdateItemPriceInBasketResult> Handle(UpdateItemPriceInBasketCommand command, CancellationToken cancellationToken)
        {
            //Find shopping cart items with a given ProductId
            var itemsToUpdate = await basketDbContext.ShoppingCartItems
                                .Where(x=>x.ProductId == command.ProductId)
                                .ToListAsync(cancellationToken);
            if (!itemsToUpdate.Any())
            {
                return new UpdateItemPriceInBasketResult(false);
            }
            //Iterate over items and update price of every item with incoming command.Price 
            foreach (var item in itemsToUpdate)
            {
                item.UpdatePrice(command.Price);
            }
            //save to database
            await basketDbContext.SaveChangesAsync(cancellationToken);
            //return result
            return new UpdateItemPriceInBasketResult(true);
        }
    }
}
