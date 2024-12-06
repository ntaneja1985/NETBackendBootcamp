

namespace Catalog.Products.Features.DeleteProduct
{
    public record DeleteProductCommand(Guid productId) : ICommand<DeleteProductResult>;
    public record DeleteProductResult(bool isSuccess);
    internal class DeleteProductHandler(CatalogDbContext dbContext) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            //fetch product entity from command object
            var product = await dbContext.Products.FindAsync([command.productId], cancellationToken);
            if (product is null)
            {
                throw new Exception($"Product not found: {command.productId}");
            }

            //delete the product
            dbContext.Products.Remove(product);
            //save changes to db
            await dbContext.SaveChangesAsync(cancellationToken);
            //return the result
            return new DeleteProductResult(true);
        }
    }
}
