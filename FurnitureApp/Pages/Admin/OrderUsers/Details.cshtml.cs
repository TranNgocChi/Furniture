using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FurnitureApp;
using FurnitureApp.Models;
using DataAccess.Repository.IRepository;

namespace FurnitureApp.Pages.Admin.OrderUsers
{
    public class DetailsModel(IOrderRepository _orderRepository) : PageModel
    {
        public Order Order { get; set; } = default!;

        public IActionResult OnGet(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = _orderRepository.GetById(id.ToString());
            if (order == null)
            {
                return NotFound();
            }
            else
            {
                Order = order;
            }
            return Page();
        }
    }
}
