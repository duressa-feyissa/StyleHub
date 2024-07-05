namespace backend.Application.DTO.Product.CategoryDTO.DTO
{
	public class CreateCategoryDTO
	{
		public required string Name { get; set; }
		public required string Image { get; set; }
		public Dictionary<string, List<string>> Domain { get; set; } = new Dictionary<string, List<string>>();
	}
}