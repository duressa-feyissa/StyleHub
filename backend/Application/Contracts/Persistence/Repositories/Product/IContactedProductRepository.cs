using backend.Application.Contracts.Persistence.Repositories.Common;
using backend.Domain.Entities.Product;
using Task = Twilio.TwiML.Voice.Task;

namespace backend.Application.Contracts.Persistence.Repositories.Product;

public interface IContactedProductRepository
{
    Task<int> GetProductContactCountAsync(string productId);
    Task<bool> IsProductContactedAsync(ContactedProduct contactedProduct);
}