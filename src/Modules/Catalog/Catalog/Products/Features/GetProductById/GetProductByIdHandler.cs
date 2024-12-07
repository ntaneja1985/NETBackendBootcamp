
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Catalog.Products.Features.GetProductById
{
    public record GetProductByIdQuery(Guid id) : IQuery<GetProductByIdResult>;

    public record GetProductByIdResult(ProductDto Product);
    internal class GetProductByIdHandler(CatalogDbContext dbContext) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            var product = await dbContext.Products
                            .AsNoTracking()
                           .SingleOrDefaultAsync(x=>x.Id == query.id, cancellationToken);

            if (product == null)
            {
                // throw new Exception($"Product not found: {query.id}");
                throw new ProductNotFoundException(query.id);
            }

            var productDto = product.Adapt<ProductDto>();

            return new GetProductByIdResult(productDto);
        }
    }
    
}
