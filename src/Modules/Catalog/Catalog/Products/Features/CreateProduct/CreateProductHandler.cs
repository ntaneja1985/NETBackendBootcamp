
namespace Catalog.Products.Features.CreateProduct
{

    public record CreateProductCommand
        (ProductDto Product)
        :ICommand<CreateProductResult>;

    public record CreateProductResult(Guid Id);

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Product.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Product.Category).NotEmpty().WithMessage("Category is required");
            RuleFor(x => x.Product.ImageFile).NotEmpty().WithMessage("Image File is required");
            RuleFor(x => x.Product.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }
    internal class CreateProductHandler(CatalogDbContext dbContext
        ,ILogger<CreateProductHandler> logger) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            
            //Logging 
            //Use Logging Behavior
            //logger.LogInformation("Create Product Command Handler handle method called with {@Command}", command);


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
