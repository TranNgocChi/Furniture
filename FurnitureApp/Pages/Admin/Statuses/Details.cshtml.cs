using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FurnitureApp;
using FurnitureApp.Models;

namespace FurnitureApp.Pages.Admin.Statuses
{
    public class DetailsModel : PageModel
    {
        private readonly FurnitureApp.AppDbContext _context;

        public DetailsModel(FurnitureApp.AppDbContext context)
        {
            _context = context;
        }

        public Status Status { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var status = await _context.Statuses.FirstOrDefaultAsync(m => m.Id == id);
            if (status == null)
            {
                return NotFound();
            }
            else
            {
                Status = status;
            }
            return Page();
        }
    }
}
