namespace Application.DTO.LocationDTO.DTO
{
	public interface IBaseLocationDTO
	{
		string Name { get; set; }
		double Latitude { get; set; }
		double Longitude { get; set; }
	}
}