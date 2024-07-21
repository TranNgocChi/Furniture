using System.ComponentModel.DataAnnotations;

namespace FurnitureApp.Models
{
	public class Address
	{
		[Key]
		public Guid Id { get; set; } = Guid.NewGuid();
		public string? Country { get; set; }
		public string? City { get; set; }
		public string? District { get; set; }
		public string? Town { get; set; }
		public string? Detail { get; set; }
		public string? Phone { get; set; }
	}
}
