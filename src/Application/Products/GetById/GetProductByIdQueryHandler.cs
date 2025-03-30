using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Product;
using Domain.Products;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Products.GetById;
internal sealed class GetProductByIdQueryHandler(IApplicationDbContext _dbContext)
    : IQueryHandler<GetProductByIdQuery, ProductResponse>
{
    public async Task<Result<ProductResponse>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        Product product = await _dbContext.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (product == null)
        {
            return Result.Failure<ProductResponse>(ProductErrors.NotFound(request.Id));
        }

        var response = new ProductResponse
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Images = product.Images,
            Price = product.Price,
            Active = product.Active,
            Updated = product.Updated
        };

        return Result.Success(response);
    }
}
