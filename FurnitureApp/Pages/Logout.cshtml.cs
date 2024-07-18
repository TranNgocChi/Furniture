using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Distributed;

namespace FurnitureApp.Pages
{
	public class LogoutModel(IDistributedCache _cache, IHttpContextAccessor _httpContextAccessor) : PageModel
	{
		public async Task<IActionResult> OnGetAsync()
		{
			try
			{
				var sessionId = _httpContextAccessor.HttpContext?.Request.Cookies["sessionId"];
				if (!string.IsNullOrEmpty(sessionId))
				{
					await _cache.RemoveAsync(sessionId);
					Response.Cookies.Delete(sessionId);
				}
				return RedirectToPage("/Index");
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}
