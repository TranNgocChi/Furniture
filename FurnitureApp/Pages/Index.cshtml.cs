using DataAccess.Repository.IRepository;
using FurnitureApp.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using static FurnitureApp.Pages.Shared._HeaderModel;

namespace FurnitureApp.Pages
{
	public class IndexModel(ILogger<IndexModel> logger, IDistributedCache cache, IProductRepository productRepository) : PageModel
	{
		private readonly ILogger<IndexModel> _logger = logger;
		private readonly IDistributedCache _cache = cache;
		private readonly int PREVIEW_PRODUCTS_NUMBER = 3;
		private readonly IProductRepository productRepository = productRepository;

		public string? UserName { get; set; }
		public bool IsUserLogined { get; set; }

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
			ViewData["Header"] = headerModel;
		}

	}
}