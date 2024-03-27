namespace Domain.Entities
{
	public class Location
	{
		public string Id { get; set; } = Guid.NewGuid().ToString();
		public required string Name { get; set; }
		public required double Latitude { get; set; }
		public required double Longitude { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.Now;
		public DateTime UpdatedAt { get; set; } = DateTime.Now;
	}
}
