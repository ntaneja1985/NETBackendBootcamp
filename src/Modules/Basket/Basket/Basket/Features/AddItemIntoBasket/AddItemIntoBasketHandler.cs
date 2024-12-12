

namespace Basket.Basket.Features.AddItemIntoBasket
{
    public record AddItemIntoBasketCommand(string UserName, ShoppingCartItemDto ShoppingCartItem)
    : ICommand<AddItemIntoBasketResult>;
    public record AddItemIntoBasketResult(Guid Id);
    public class AddItemIntoBasketCommandValidator : AbstractValidator<AddItemIntoBasketCommand>
    {
        public AddItemIntoBasketCommandValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is required");
            RuleFor(x => x.ShoppingCartItem.ProductId).NotEmpty().WithMessage("ProductId is required");
            RuleFor(x => x.ShoppingCartItem.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than 0");
        }
    }
    internal class AddItemIntoBasketHandler(IBasketRepository repository, ISender sender)
    : ICommandHandler<AddItemIntoBasketCommand, AddItemIntoBasketResult>
    {
        public async Task<AddItemIntoBasketResult> Handle(AddItemIntoBasketCommand command, CancellationToken cancellationToken)
        {
            // Add shopping cart item into shopping cart
            //var shoppingCart = await dbContext.ShoppingCarts
            //    .Include(x => x.Items)
            //    .SingleOrDefaultAsync(x => x.UserName == command.UserName, cancellationToken);

            //if (shoppingCart is null)
            //{
            //    throw new BasketNotFoundException(command.UserName);
            //}

            var shoppingCart = await repository.GetBasket(command.UserName,false,cancellationToken);

            //TODO: Before adding item to shopping cart, we should call the Catalog Module GetProductById method
            //Get latest product information and set Price and ProductName when adding item into the shopping cart.
            var result = await sender.Send(new GetProductByIdQuery(command.ShoppingCartItem.ProductId));

            shoppingCart.AddItem(
                command.ShoppingCartItem.ProductId,
                command.ShoppingCartItem.Quantity,
                command.ShoppingCartItem.Color,
                //command.ShoppingCartItem.Price,
                //command.ShoppingCartItem.ProductName
                result.Product.Price,
                result.Product.Name
                );

            //await dbContext.SaveChangesAsync(cancellationToken);
            await repository.SaveChangesAsync(command.UserName, cancellationToken);

            return new AddItemIntoBasketResult(shoppingCart.Id);
        }
    }
}
