using backend.Domain.Common;

namespace backend.Domain.Entities.Common
{
    public class Location : BaseEntity
    {
        public required string Name { get; set; }
        public required double Latitude { get; set; }
        public required double Longitude { get; set; }
    }
}
