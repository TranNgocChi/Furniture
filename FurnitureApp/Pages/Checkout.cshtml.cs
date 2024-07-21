using DataAccess.DAO;
using DataAccess.Repository.CRepository;
using DataAccess.Repository.IRepository;
using FurnitureApp.Helpers;
using FurnitureApp.Models;
using FurnitureApp.Models.Admin;
using FurnitureApp.Models.VNMaps;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static FurnitureApp.Pages.Shared._HeaderModel;

namespace FurnitureApp.Pages;

public class CheckoutModel(ISessionHelper _sessionHelper, IUserRepository _userRepository, 
	ICartRepository _cartRepository, ICartItemRepository _cartItemRepository,
	IOrderRepository _orderRepository, IStatusRepository _statusRepository) : PageModel
{
	private readonly VnMapContext _vnMapContext = new();
	public ShopAddress? ShopAddress { get; set; }
	public User? User { get; set; }
	public Cart? Cart { get; set; }
	public List<CartItem>? CartItems { get; set; }
	public List<Province>? Provinces { get; set; }
	public List<District>? Districts { get; set; }
	public List<Ward>? Wards { get; set; }
	public async Task<IActionResult> OnGetAsync(string selectedCity)
	{
		var session = await _sessionHelper.GetSessionAsync(Request);
		if (session == null)
		{
			return Redirect("/");
		}

		User = _userRepository.GetByEmail(session.UserEmail ?? string.Empty);
		if (User == null)
		{
			return Redirect("/");
		}

		Cart = _cartRepository.GetByUserId(User.Id.ToString()).FirstOrDefault();
		if (Cart == null)
		{
			return Redirect("/");
		}

		List<CartItem> cartItems = _cartItemRepository.GetAllByCartId(Cart.Id.ToString());
		if (cartItems == null || cartItems.Count == 0)
		{
			return Redirect("/");
		}

		CartItems = cartItems;

		Provinces = _vnMapContext.Provinces.ToList();
		ShopAddress = new ShopAddress
		{
			Country = "Viet Nam",
			City = "Da Nang",
			District = "Ngu Hanh Son",
			Ward = "Hoa Hai",
			DetailAddress = "Dai Hoc FPT"
		};
		ViewData["Header"] = session;
		return Page();	
	}

	public async Task<IActionResult> OnPostAsync(string c_country, string c_city, string c_district, string c_ward,
			string c_detail, string c_phone, string c_order_notes, string payment_method, decimal totalPrice,
			decimal shipping)
	{
        var session = await _sessionHelper.GetSessionAsync(Request);
        if (session == null)
        {
            return Redirect("/");
        }

        User = _userRepository.GetByEmail(session.UserEmail ?? string.Empty);
        if (User == null)
        {
            return Redirect("/");
        }

        Cart = _cartRepository.GetByUserId(User.Id.ToString()).FirstOrDefault();
        if (Cart == null)
        {
            return Redirect("/");
        }

        List<CartItem> cartItems = _cartItemRepository.GetAllByCartId(Cart.Id.ToString());
        if (cartItems == null || cartItems.Count == 0)
        {
            return Redirect("/");
        }

        CartItems = cartItems;

        // Create address for order
        var orderAddress = new Address
		{
			Country = c_country,
			City = c_city,
			District = c_district,
			Town = c_ward,
			Detail = c_detail,
			Phone = c_phone,
		};

		// Create order item 
		var orderItems = new List<OrderItem>();
		if (CartItems != null)
		{
			foreach (var cartitem in CartItems)
			{
				orderItems.Add(new OrderItem
				{
					Product = cartitem.Product,
					Quantity = cartitem.Quantity,
				});
			}
		}

		var order = new Order
		{
			ShippingPrice = shipping,
			OrderTotal = totalPrice,
			OrderAddress = orderAddress,
			UserOrder = User,
			PaymentMethod = payment_method,
			OrderNote = c_order_notes,
			OrderDate = DateTime.Now,
			Status = _statusRepository.GetAll().FirstOrDefault(),
			OrderItems = orderItems
		};

		if (payment_method == "payment_delivery")
		{
			_orderRepository.Create(order);
			string cartId = Cart.Id.ToString();
			if (cartId == null)
			{
				return Redirect("/");
			}
			Cart.CartTotal = 0;
			_cartRepository.Update(Cart);
			_cartItemRepository.DeleteAllByCartId(cartId);
			return Redirect("/OrderStatus/OrderSuccess");
		}
		else if (payment_method == "payment_online")
		{
			return Redirect("/OrderStatus/OrderSuccess");
		}

		return Redirect("/OrderStatus/OrderFail");
	}
}
