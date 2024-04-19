namespace backend.Application.DTO.Common.Location.DTO
{
    public class LocationResponseDTO
    {
        public required string Id { get; set; }
        public required string Name { get; set; }

        public required double Latitude { get; set; }

        public required double Longitude { get; set; }

    }
}
