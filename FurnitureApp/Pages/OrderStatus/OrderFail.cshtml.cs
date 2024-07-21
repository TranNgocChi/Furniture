using FurnitureApp.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FurnitureApp.Pages.OrderStatus;

public class OrderFailModel(ISessionHelper _sessionHelper) : PageModel
{
	public async Task<IActionResult> OnGetAsync()
	{
		var session = await _sessionHelper.GetSessionAsync(Request);
		if (session == null)
		{
			return Redirect("/");
		}
		ViewData["Header"] = session;
		return Page();
	}
}
