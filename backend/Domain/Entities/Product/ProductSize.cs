using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Domain.Entities.Product
{
	public class ProductSize
	{
		public string Id { get; set; } = Guid.NewGuid().ToString();

		[ForeignKey("Product")]
		public required string ProductId { get; set; }

		[Required]
		public required virtual Size Size { get; set; }
	}
}
