
namespace Application.DTO.LocationDTO.DTO
{
	public class BaseLocationDTO : IBaseLocationDTO
	{
		public required string Name { get; set; }
		public required double Latitude { get; set; }
		public required double Longitude { get; set; }
	}
}