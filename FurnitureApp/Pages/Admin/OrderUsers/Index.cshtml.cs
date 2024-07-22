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
    public class IndexModel(IOrderRepository _orderRepository) : PageModel
    {
        public IList<Order> Order { get;set; } = default!;

        public void OnGet()
        {
            Order = _orderRepository.GetAll();
        }
    }
}
