using Application.Contracts.Persistence.Repositories.Common;

namespace Application.Contracts.Persistence.Repositories.Product
{
    public interface IProductRepository : IGenericRepository<Domain.Entities.Product.Product>
    {
        Task<IReadOnlyList<Domain.Entities.Product.Product>> GetAll(
            string search = "",
            string? brandId = null,
            IEnumerable<string>? colorIds = null,
            IEnumerable<string>? materialIds = null,
            IEnumerable<string>? sizeIds = null,
            bool? isNegotiable = null,
            float? minPrice = null,
            float? maxPrice = null,
            int? minQuantity = null,
            int? maxQuantity = null,
            string? target = null,
            string? condition = null,
            double? latitude = null,
            double? longitude = null,
            double? radiusInKilometers = null,
            int skip = 0,
            int limit = 10
        );
        Task<Domain.Entities.Product.Product> GetById(string id);
    }
}
