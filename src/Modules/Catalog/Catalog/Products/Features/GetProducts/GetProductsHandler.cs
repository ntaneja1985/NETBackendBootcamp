

namespace Catalog.Products.Features.GetProducts
{
    public record GetProductsQuery():IQuery<GetProductsResult>;
    public record GetProductsResult(IEnumerable<ProductDto> products);
    internal class GetProductsHandler(CatalogDbContext dbContext) : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            //get products using dbContext
            var products = await dbContext.Products
                            .AsNoTracking()
                            .OrderBy(x=>x.Name)
                            .ToListAsync(cancellationToken);

            //mapping product entity to productDto using Mapster
            var productDtos = products.Adapt<List<ProductDto>>();
            
            //return result
            return new GetProductsResult(productDtos);
        }
    }
}
