using FurnitureApp.Models.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace FurnitureApp.Pages.Admin.Products
{
    public class DeleteModel : PageModel
    {
        private readonly FurnitureApp.AppDbContext _context;
        private readonly IHubContext<SignalRServer> hubContext;

        public DeleteModel(FurnitureApp.AppDbContext context, IHubContext<SignalRServer> hubContext)
        {
            _context = context;
            this.hubContext = hubContext;
        }

        [BindProperty]
        public ProductUpdateDto Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(m => m.Id == id);

            if (product == null)
            {
                return NotFound();
            }
            else
            {
                Product = new ProductUpdateDto
                {
                    Id = product.Id,
                    ProductName = product.ProductName,
                    ProductDescription = product.ProductDescription,
                    ProductPrice = product.ProductPrice,
                    Quantity = product.Quantity,
                    ProductImage = product.ProductImage,
                    CategoryId = product.Category.Id.ToString()
                };
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }

            await hubContext.Clients.All.SendAsync("LoadProducts");

            return RedirectToPage("./Index");
        }
    }
}
