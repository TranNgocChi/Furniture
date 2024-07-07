using Microsoft.AspNetCore.Mvc;
namespace FurnitureApp.Pages.Shared;

public class _HeaderModel : ViewComponent
{
	public class HeaderModel
	{
		public string? UserName { get; set; }
		public bool? IsUserLogined { get; set; }
	}
}
