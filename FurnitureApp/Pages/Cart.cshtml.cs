using FurnitureApp.Pages.Shared;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FurnitureApp.Pages
{
	public class CartModel : PageModel
	{
		public void OnGet(_HeaderModel.HeaderModel header)
		{
			ViewData["Header"] = header;
		}
	}
}
