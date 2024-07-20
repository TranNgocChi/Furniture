using FurnitureApp.Helpers;
using FurnitureApp.Models.VNMaps;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FurnitureApp.Pages.Country
{
    public class SelectedCityModel : PageModel
    {
		private readonly VnMapContext _vnMapContext = new();
		public List<Province>? Provinces { get; set; }
		public List<District>? Districts { get; set; }
		public IActionResult OnGet(string selectedCity)
		{
			if (!selectedCity.Equals("00"))
			{
				Provinces = _vnMapContext.Provinces.ToList();
				Districts = _vnMapContext.Districts.ToList();

				Districts = (from province in Provinces
							 join district in Districts
							 on province.Code equals district.ProvinceCode
							 where province.Code == selectedCity
							 select new District
							 {
								 Code = district.Code
							 ,
								 Name = district.Name
							 }).ToList();
			}
			else
			{
				var nullDistrict = new List<District>();
				nullDistrict.Add(new District { Code = "000", Name = "Select a district" });
				Districts = nullDistrict;
			}

			return new JsonResult(new { districtFounds = Districts });
		}
    }
}
