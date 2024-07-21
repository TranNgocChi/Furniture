using DataAccess.Repository.IRepository;
using FurnitureApp.Helpers;
using FurnitureApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FurnitureApp.Pages;

public class OrderModel(ISessionHelper _sessionHelper, IUserRepository _userRepository,
    IOrderRepository _orderRepository) : PageModel
{
    public List<Order>? Orders { get; set; }
    public List<OrderItem>? OrderItems { get; set; }
    public Address? Address { get; set; }
    public Status? Status { get; set; }
    public async Task<IActionResult> OnGetAsync()
    {
        var session = await _sessionHelper.GetSessionAsync(Request);
        if (session == null)
        {
            return Redirect("/");
        }

        if (session.UserEmail == null)
        {
            return Redirect("/");
        }
        var user = _userRepository.GetByEmail(session.UserEmail);

        if (user == null)
        {
            return Redirect("/");
        }

        Orders = _orderRepository.GetAll().Where(o => o.UserOrder?.Id == user.Id)
                .OrderByDescending(o => o.OrderDate).ToList();
        ViewData["Header"] = session;
        return Page();
    }
}
