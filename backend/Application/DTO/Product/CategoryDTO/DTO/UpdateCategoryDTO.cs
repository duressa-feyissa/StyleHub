namespace backend.Application.DTO.Product.CategoryDTO.DTO
{
	public class UpdateCategoryDTO
	{
		public string? Name { get; set; }
		public string? Image { get; set; }
		public Dictionary<string, List<string>>? Domain { get; set; } = new Dictionary<string, List<string>>();
	}
}
