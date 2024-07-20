using FurnitureApp.Helpers;
using FurnitureApp.Models.VNMaps;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FurnitureApp.Pages
{
	public class CheckoutModel(ISessionHelper _sessionHelper) : PageModel
	{
		private readonly VnMapContext _vnMapContext = new();
		public List<Province>? Provinces { get; set; }
		public List<District>? Districts { get; set; }
		public List<Ward>? Wards { get; set; }
		public async Task OnGet(string selectedCity)
		{
			Provinces = _vnMapContext.Provinces.ToList();
			
			ViewData["Header"] = await _sessionHelper.GetSessionAsync(Request);
		}
	}
}
