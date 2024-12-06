

namespace Catalog.Products.Features.CreateProduct
{

    public record CreateProductCommand
        (ProductDto Product)
        :ICommand<CreateProductResult>;

    public record CreateProductResult(Guid Id);
    internal class CreateProductHandler(CatalogDbContext dbContext) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {

            //create Product Entity from command object
            var product = CreateNewProduct(command.Product);
            //save to database
            dbContext.Products.Add(product);
            await dbContext.SaveChangesAsync(cancellationToken);

            //return a result
            return new CreateProductResult(product.Id);
        }

        private Product CreateNewProduct(ProductDto productDto)
        {
            var product = Product.Create(
                Guid.NewGuid(),
                productDto.Name,
                productDto.Category,
                productDto.Description,
                productDto.ImageFile,
                productDto.Price);

            return product;
        }
    }
}
