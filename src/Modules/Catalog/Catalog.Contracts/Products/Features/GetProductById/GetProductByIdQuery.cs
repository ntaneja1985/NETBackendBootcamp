

namespace Catalog.Contracts.Products.Features.GetProductById
{
    public record GetProductByIdQuery(Guid id) : IQuery<GetProductByIdResult>;

    public record GetProductByIdResult(ProductDto Product);
}
