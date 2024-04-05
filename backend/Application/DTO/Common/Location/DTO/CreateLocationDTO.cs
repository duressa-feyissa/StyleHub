namespace Application.DTO.Common.Location.DTO
{
    public class CreateLocationDTO
    {
        public required string Name { get; set; }
        public required double Latitude { get; set; }
        public required double Longitude { get; set; }
    }
}
