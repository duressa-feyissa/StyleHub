namespace backend.Application.DTO.Product.CategoryDTO.DTO
{
	public class CategoryResponseDTO
	{
		public string Id { get; set; } = string.Empty;
		public string Name { get; set; } = string.Empty;
		public string Image { get; set; } = string.Empty;
		public Dictionary<string, List<string>>? Domain { get; set; } = new Dictionary<string, List<string>>();
		
	}
}
