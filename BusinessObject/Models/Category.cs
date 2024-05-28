using System.ComponentModel.DataAnnotations;

namespace FurnitureApp.Models
{
	public class Category
	{
		[Key]
		public Guid Id { get; set; } = Guid.NewGuid();

		[Required(ErrorMessage = "Category Name is required")]
		[StringLength(50)]	
		public string? CategoryName { get; set; }
		public virtual ICollection<Product>? Products { get; set; }

	}
}
