using FurnitureApp.Models;
using FurnitureApp.Models.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace FurnitureApp.Pages.Admin.Products
{
    public class CreateModel : PageModel
    {
        private readonly FurnitureApp.AppDbContext _context;
        private readonly IHubContext<SignalRServer> hubContext;

        public CreateModel(FurnitureApp.AppDbContext context, IHubContext<SignalRServer> hubContext)
        {
            _context = context;
            this.hubContext = hubContext;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Categories = await _context.Categories.ToListAsync();
            return Page();
        }

        [BindProperty]
        public ProductCreateDto Product { get; set; } = default!;

        public List<Category> Categories { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
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
                    ProductName = Product.ProductName,
                    ProductDescription = Product.ProductDescription,
                    ProductPrice = Product.ProductPrice,
                    Quantity = Product.Quantity,
                    ProductImage = Product.ProductImage,
                    Category = category
                };

                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                await hubContext.Clients.All.SendAsync("LoadProducts");
            }
            return RedirectToPage("Index");
        }
    }
}
