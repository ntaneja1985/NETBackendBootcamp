


namespace Catalog.Products.Features.GetProductByCategory
{
    public record GetProductByCategoryQuery(string category): IQuery<GetProductByCategoryResult>;

    public record GetProductByCategoryResult(IEnumerable<ProductDto> products);
    internal class GetProductsByCategoryHandler(CatalogDbContext dbContext) : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
        {
            var products = await dbContext.Products
                           .AsNoTracking()
                           .Where(x=>x.Category.Contains(query.category))  
                           .OrderBy(x=>x.Name)
                           .ToListAsync(cancellationToken);
            var productDtos = products.Adapt<List<ProductDto>>();
            return new GetProductByCategoryResult(productDtos);
        }
    }
}
