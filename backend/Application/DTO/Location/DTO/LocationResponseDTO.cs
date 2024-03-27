namespace Application.DTO.LocationDTO.DTO
{
    public class LocationResponseDTO : IBaseLocationDTO
    {
        public required string Id { get; set; }
        public required string Name { get; set; }

        public required double Latitude { get; set; }

        public required double Longitude { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
