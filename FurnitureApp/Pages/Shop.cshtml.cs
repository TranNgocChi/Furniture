using DataAccess.Repository.IRepository;
using FurnitureApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FurnitureApp.Pages
{
    public class ShopModel : PageModel
    {
        private readonly IProductRepository _productRepository;
        public ShopModel(IProductRepository productRepository)
        {
            _productRepository = productRepository; 
        }

        public List<Product> products { get; set; }
        public void OnGet()
        {
            products = _productRepository.GetAll(); 
        }
    }
}
