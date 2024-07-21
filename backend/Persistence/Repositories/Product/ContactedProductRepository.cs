using backend.Application.Contracts.Persistence.Repositories.Product;
using backend.Domain.Entities.Product;
using backend.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;

namespace backend.Persistence.Repositories.Product;

public class ContactedProductRepository(StyleHubDBContext context)
    :  IContactedProductRepository
{
    public async Task<int> GetProductContactCountAsync(string productId)
    {
        return await context.ContactedProducts
            .Where(x => x.ProductId == productId)
            .CountAsync();
    }

    public async Task<bool> IsProductContactedAsync(ContactedProduct contactedProduct)
    {
        var existingContactedProduct = await context.ContactedProducts
            .Where(x => x.ProductId == contactedProduct.ProductId && x.UserId == contactedProduct.UserId)
            .FirstOrDefaultAsync();

        if (existingContactedProduct == null)
        {
            await context.ContactedProducts.AddAsync(contactedProduct);
            await context.SaveChangesAsync();
            return true;
        } else {
            return false;
        }
    }
}