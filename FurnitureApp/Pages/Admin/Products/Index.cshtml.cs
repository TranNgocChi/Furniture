using FurnitureApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FurnitureApp.Pages.Admin.Products
{
    public class IndexModel : PageModel
    {
        private readonly FurnitureApp.AppDbContext _context;

        public IndexModel(FurnitureApp.AppDbContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Product = await _context.Products.ToListAsync();
        }

        public async Task<ActionResult> OnGetGetProductsAsync()
        {
            var product = await _context.Products.ToListAsync();
            return new OkObjectResult(product);
        }
    }
}
