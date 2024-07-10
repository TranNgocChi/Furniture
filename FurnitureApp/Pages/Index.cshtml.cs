using DataAccess.Repository.IRepository;
using FurnitureApp.Models;
using FurnitureApp.Pages.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text;
using static FurnitureApp.Pages.Shared._HeaderModel;

namespace FurnitureApp.Pages
{
    public class IndexModel(ILogger<IndexModel> logger,
        IDistributedCache cache,
        IProductRepository productRepository,
        IUserRepository userRepository) : PageModel
    {
        private readonly ILogger<IndexModel> _logger = logger;
        private readonly IDistributedCache _cache = cache;
        private readonly int PREVIEW_PRODUCTS_NUMBER = 3;
        private readonly IProductRepository productRepository = productRepository;
        private readonly IUserRepository userRepository = userRepository;

        public string? UserName { get; set; }
        public bool IsUserLogined { get; set; }
        public List<CartItem> MyProperty { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();

        public async Task OnGet()
        {
            Products = productRepository.GetAll().Take(PREVIEW_PRODUCTS_NUMBER).ToList();

            var sessionId = Request.Cookies["sessionId"];
            if (!String.IsNullOrEmpty(sessionId))
            {
                var sessionData = await _cache.GetAsync(sessionId);
                if (sessionData != null)
                {
                    UserName = Encoding.UTF8.GetString(sessionData);
                    IsUserLogined = true;
                }
            }
            else
            {
                IsUserLogined = false;
            }
            var headerModel = new HeaderModel
            {
                UserName = UserName,
                IsUserLogined = IsUserLogined
            };
            //ViewData["Header"] = headerModel;


            // Below code is used to fake data for current user
            var currentUser = userRepository.GetByEmail("nvdkhoa.dev@gmail.com");

            ViewData["Header"] = new HeaderModel
            {
                UserName = currentUser.UserName,
                IsUserLogined = true
            };
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