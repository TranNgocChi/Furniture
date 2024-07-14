using DataAccess.Repository.IRepository;
using FurnitureApp.Helpers;
using FurnitureApp.Models;
using FurnitureApp.Pages.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace FurnitureApp.Pages
{
    public class IndexModel(ILogger<IndexModel> logger,
        IDistributedCache cache,
        IProductRepository productRepository,
        IUserRepository userRepository, ISessionHelper sessionHelper) : PageModel
    {
        private readonly ILogger<IndexModel> _logger = logger;
        private readonly IDistributedCache _cache = cache;
        private readonly int PREVIEW_PRODUCTS_NUMBER = 3;
        private readonly IProductRepository productRepository = productRepository;
        private readonly IUserRepository userRepository = userRepository;
        private readonly ISessionHelper _sessionHelper = sessionHelper;
        public string? SessionData { get; }
        public List<CartItem> MyProperty { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();

        public async Task OnGet()
        {
            Products = productRepository.GetAll().Take(PREVIEW_PRODUCTS_NUMBER).ToList();
            ViewData["Header"] = await _sessionHelper.GetSessionAsync(Request);
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