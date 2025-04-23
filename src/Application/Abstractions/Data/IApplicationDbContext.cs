using Domain.Products;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<Product> Products { get; }
    DbSet<ProductPrice> ProductPrices { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
