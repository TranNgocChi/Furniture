using DataAccess.Repository.IRepository;
using FurnitureApp.Models;
using FurnitureApp.Pages.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace FurnitureApp.Pages.Shop
{
    public class IndexModel : PageModel
    {
        private readonly IProductRepository _productRepository;
        public IndexModel(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public List<Product> products { get; set; }
        public void OnGet(string headerJson)
        {
            products = _productRepository.GetAll();
            var header = JsonConvert.DeserializeObject<_HeaderModel.HeaderModel>(headerJson);
            ViewData["Header"] = header;
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
}
