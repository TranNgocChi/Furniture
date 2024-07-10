using DataAccess.Repository.IRepository;
using FurnitureApp.Models;
using FurnitureApp.Pages.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace FurnitureApp.Pages.Shop
{
    public class ProductDetailModel(IProductRepository productRepository) : PageModel
    {
        private readonly IProductRepository _productRepository = productRepository;
        public Product Product { get; set; }

        public void OnGet(string productId, string headerJson)
        {
            Product = _productRepository.GetById(productId);
            var header = JsonConvert.DeserializeObject<_HeaderModel.HeaderModel>(headerJson);
            ViewData["Header"] = header;
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
