using DataAccess.Repository.IRepository;
using FurnitureApp.Models;
using FurnitureApp.Pages.Shared;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FurnitureApp.Pages.Shop
{
	public class ProductDetailModel(IProductRepository productRepository) : PageModel
	{
		private readonly IProductRepository _productRepository = productRepository;
		public Product Product { get; set; }

		public void OnGet(string productId, _HeaderModel.HeaderModel header)
		{
			Product = _productRepository.GetById(productId);
			ViewData["Header"] = header;
		}
	}
}
