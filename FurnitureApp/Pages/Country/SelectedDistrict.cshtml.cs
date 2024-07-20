using FurnitureApp.Models.VNMaps;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FurnitureApp.Pages.Country
{
    public class SelectedDistrictModel : PageModel
    {
		private readonly VnMapContext _vnMapContext = new();
		public List<District>? Districts { get; set; }
		public List<Ward>? Wards { get; set; }
		public IActionResult OnGet(string selectedDistrict)
        {
			if (!string.IsNullOrEmpty(selectedDistrict) && !selectedDistrict.Equals("000"))
			{
				Districts = _vnMapContext.Districts.ToList();
				Wards = _vnMapContext.Wards.ToList();

				Wards = (from district in Districts
						 join ward in Wards
							on district.Code equals ward.DistrictCode
							where district.Code == selectedDistrict
							select new Ward
							{
								Code = ward.Code
							,
								Name = ward.Name
							}).ToList();
				
			}
			else
			{
				var nullWard = new List<Ward>();
				nullWard.Add(new Ward { Code = "0000", Name = "Select a ward" });
				Wards = nullWard;
			}
			return new JsonResult(new { wardFounds = Wards });
		}
    }
}
