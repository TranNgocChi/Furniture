using DataAccess.Repository.IRepository;
using FurnitureApp.Helpers;
using FurnitureApp.Models;
using FurnitureApp.Pages.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace FurnitureApp.Pages.Shop;

public class IndexModel : PageModel
{
    private readonly IProductRepository _productRepository;
		private readonly ISessionHelper _sessionHelper;
		public IndexModel(IProductRepository productRepository, ISessionHelper sessionHelper)
    {
        _productRepository = productRepository;
        _sessionHelper = sessionHelper;
    }

    public List<Product> products { get; set; }
    public async Task OnGet()
    {
        products = _productRepository.GetAll();
        ViewData["Header"] = await _sessionHelper.GetSessionAsync(Request);

		}

    public ActionResult OnPostViewDetail(string productId)
    {
        return RedirectToPage("ItemDetail", new { productId = productId });
    }

    public ActionResult OnPostAddToCart(string productJson, string headerJson)
    {
        var selectedProduct = JsonConvert.DeserializeObject<Product>(productJson);
        var header = JsonConvert.DeserializeObject<_HeaderModel.HeaderModel>(headerJson);
        if (selectedProduct is not null)
        {
            return RedirectToPage("/Cart", new { addedProductJson = productJson, headerJson = headerJson });
        }
        return Page();
    }
}
