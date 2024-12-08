

using Shared.Pagination;

namespace Catalog.Products.Features.GetProducts
{
    public record GetProductsQuery(PaginationRequest PaginationRequest):IQuery<GetProductsResult>;
    public record GetProductsResult(PaginatedResult<ProductDto> Products);
    internal class GetProductsHandler(CatalogDbContext dbContext) : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            var pageIndex = query.PaginationRequest.PageIndex;
            var pageSize = query.PaginationRequest.PageSize;
            var totalCount = await dbContext.Products.LongCountAsync(cancellationToken);
            //get products using dbContext
            var products = await dbContext.Products
                            .AsNoTracking()
                            .Skip((pageIndex - 1)*pageSize)
                            .Take(pageSize)
                            .OrderBy(x=>x.Name)
                            .ToListAsync(cancellationToken);

            //mapping product entity to productDto using Mapster
            var productDtos = products.Adapt<IEnumerable<ProductDto>>();
            
            //return result
            return new GetProductsResult(new PaginatedResult<ProductDto>(pageIndex,pageSize,totalCount,productDtos));
        }
    }
}
