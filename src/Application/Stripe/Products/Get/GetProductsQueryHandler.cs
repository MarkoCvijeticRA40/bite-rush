using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Helpers;
using Domain.Products;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Stripe.Products.Get;

internal sealed class GetProductsQueryHandler(IApplicationDbContext _dbContext)
    : IQueryHandler<GetProductsQuery, List<GetProductsResponse>>
{
    public async Task<Result<List<GetProductsResponse>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        List<Product> products = await _dbContext.Products.AsNoTracking()
                                                          .Include(p => p.Prices)
                                                          .ToListAsync(cancellationToken);

        var response = products.Select(product => new GetProductsResponse
        {
            Id = product.Id,
            Name = product.Name ?? string.Empty,
            Description = product.Description ?? string.Empty,
            Images = product.Images ?? [],
            Active = product.Active,
            Updated = product.Updated,
            Price = product.Prices.First(p => p.Active).UnitAmount,
            PriceId = product.Prices.First(p => p.Active).Id,
            Currency = CurrencyHelper.GetCurrencySymbol(product.Prices.First(p => p.Active).Currency)
        }).ToList();

        return Result.Success(response);
    }
}
