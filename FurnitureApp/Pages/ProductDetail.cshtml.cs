using DataAccess.Repository.CRepository;
using DataAccess.Repository.IRepository;
using FurnitureApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FurnitureApp.Pages
{
    public class ProductDetailModel(IUserRepository userRepository, IProductRepository productRepository,
        ICategoryRepository categoryRepository, ICartRepository cartRepository) : PageModel
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IProductRepository _productRepository = productRepository;
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        private readonly ICartRepository _cartRepository = cartRepository;

        public List<User> Users { get; set; } = [];
        public List<Product> Products { get; set; } = [];
        public List<Category> Categories { get; set; } = [];
        public List<Cart> Carts { get; set; } = [];

        public void OnGet()
        {
            Categories =  _categoryRepository.GetAll();
            Products = _productRepository.GetAll();
            Carts = _cartRepository.GetAll();
            Users = _userRepository.GetAll();
        }
    }
}
