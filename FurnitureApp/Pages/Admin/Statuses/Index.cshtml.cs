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
    public class IndexModel : PageModel
    {
        private readonly FurnitureApp.AppDbContext _context;

        public IndexModel(FurnitureApp.AppDbContext context)
        {
            _context = context;
        }

        public IList<Status> Status { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Status = await _context.Statuses.ToListAsync();
        }
    }
}
