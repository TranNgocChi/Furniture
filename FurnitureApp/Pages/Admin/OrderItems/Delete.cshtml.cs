﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FurnitureApp;
using FurnitureApp.Models;

namespace FurnitureApp.Pages.Admin.OrderItems
{
    public class DeleteModel : PageModel
    {
        private readonly FurnitureApp.AppDbContext _context;

        public DeleteModel(FurnitureApp.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public OrderItem OrderItem { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderitem = await _context.OrderItems.FirstOrDefaultAsync(m => m.Id == id);

            if (orderitem == null)
            {
                return NotFound();
            }
            else
            {
                OrderItem = orderitem;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderitem = await _context.OrderItems.FindAsync(id);
            if (orderitem != null)
            {
                OrderItem = orderitem;
                _context.OrderItems.Remove(OrderItem);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
