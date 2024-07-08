using DataAccess.Repository.IRepository;
using FurnitureApp.Models;
using FurnitureApp.Pages.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
        public void OnGet(_HeaderModel.HeaderModel header)
        {
            products = _productRepository.GetAll();
            ViewData["Header"] = header;
        }

        public ActionResult OnPostViewDetail(string productId)
        {
            return RedirectToPage("ItemDetail", new { productId = productId });
        }
    }
}
