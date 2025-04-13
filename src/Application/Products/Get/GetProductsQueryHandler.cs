using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Product;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Products.Get;
internal sealed class GetProductsQueryHandler(IApplicationDbContext _dbContext)
    : IQueryHandler<GetProductsQuery, List<ProductResponse>>
{
    public async Task<Result<List<ProductResponse>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        List<Product> products = await _dbContext.Products
                .AsNoTracking()
                .ToListAsync(cancellationToken);

        var response = products.Select(product => new ProductResponse
        {
            Id = product.Id,
            Name = product.Name ?? string.Empty,
            Description = product.Description ?? string.Empty,
            Images = product.Images ?? new List<string>(),
            Price = product.Price,
            PriceId = product.PriceId,
            Active = product.Active,
            Updated = product.Updated
        }).ToList();

        return Result.Success(response);
    }
}
