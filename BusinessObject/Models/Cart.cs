using System.ComponentModel.DataAnnotations;

namespace FurnitureApp.Models
{
	public class Cart
	{
		[Key]
		public Guid Id { get; set; } = Guid.NewGuid();

		[Required]
		public decimal CartTotal {  get; set; }

		[Required]
		public virtual required User UserCart { get; set; }

		public virtual required ICollection<CartItem> CartItems { get; set;}
	}
}
