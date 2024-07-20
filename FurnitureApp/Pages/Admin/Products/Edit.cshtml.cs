using FurnitureApp.Models;
using FurnitureApp.Models.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace FurnitureApp.Pages.Admin.Products
{
    public class EditModel : PageModel
    {
        private readonly FurnitureApp.AppDbContext _context;
        private readonly IHubContext<SignalRServer> hubContext;

        public EditModel(FurnitureApp.AppDbContext context, IHubContext<SignalRServer> hubContext)
        {
            _context = context;
            this.hubContext = hubContext;
        }

        public List<Category> Categories { get; set; }

        [BindProperty]
        public ProductUpdateDto Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            Categories = await _context.Categories.ToListAsync();

            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            Product = new ProductUpdateDto
            {
                Id = product.Id,
                ProductName = product.ProductName,
                ProductDescription = product.ProductDescription,
                ProductPrice = product.ProductPrice,
                Quantity = product.Quantity,
                ProductImage = product.ProductImage,
                CategoryId = product.Category?.Id.ToString() ?? ""
            };

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id.ToString() == Product.CategoryId);

            if (category is not null)
            {
                var product = new Product
                {
                    Id = Product.Id,
                    ProductName = Product.ProductName,
                    ProductDescription = Product.ProductDescription,
                    ProductPrice = Product.ProductPrice,
                    Quantity = Product.Quantity,
                    ProductImage = Product.ProductImage,
                    Category = category
                };
                _context.Attach(product).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                    await hubContext.Clients.All.SendAsync("LoadProducts");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return RedirectToPage("./Index");
        }

        private bool ProductExists(Guid id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
