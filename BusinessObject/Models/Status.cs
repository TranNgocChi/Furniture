using System.ComponentModel.DataAnnotations;

namespace FurnitureApp.Models;

public class Status
{
	[Key]
	public Guid Id { get; set; } = Guid.NewGuid();

	[StringLength(50, ErrorMessage = "Status Order Length <= 50")]
	public string? StatusOrder { get; set; }
}
