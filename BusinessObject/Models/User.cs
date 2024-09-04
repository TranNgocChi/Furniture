using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FurnitureApp.Models;

public class User : IdentityUser
{
	[StringLength(200, ErrorMessage = "User Image Length <= 200")]
	public string? UserImage {  get; set; }
}
