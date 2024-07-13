using DataAccess.Repository.IRepository;
using FurnitureApp.Helpers;
using FurnitureApp.Models;
using FurnitureApp.Models.Cart;
using FurnitureApp.Pages.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace FurnitureApp.Pages
{
    [IgnoreAntiforgeryToken]
    public class CartModel(ICartRepository cartRepository,
        ICartItemRepository cartItemRepository,
        IUserRepository userRepository,
        IProductRepository productRepository,
        ISessionHelper sessionHelper) : PageModel
    {
        private readonly ICartRepository cartRepository = cartRepository;
        private readonly ICartItemRepository cartItemRepository = cartItemRepository;
        private readonly IUserRepository userRepository = userRepository;
        private readonly IProductRepository productRepository = productRepository;
		private readonly ISessionHelper _sessionHelper = sessionHelper;
		[BindProperty]
        public Cart CurrentCart { get; set; }
        [BindProperty]
        public List<CartItem>? CurrentCartItems { get; set; }
        public async Task OnGet(string headerJson, string? addedProductJson)
        {
            var header = await _sessionHelper.GetSessionAsync(Request);

			var currentUser = userRepository.GetAll().First(user => user.Email == header.UserEmail);

            CurrentCart = cartRepository.GetByUserId(currentUser.Id.ToString() ?? "").LastOrDefault()
                ?? new Cart { CartTotal = 0, UserCart = currentUser };
            CurrentCartItems = GetCartItems(CurrentCart?.Id.ToString() ?? "");

            if (addedProductJson is not null)
            {
                var product = JsonConvert.DeserializeObject<Product>(addedProductJson);
                if (CurrentCart.CartTotal > 0)
                {
                    CurrentCartItems = cartItemRepository.GetAllByCartId(CurrentCart.Id.ToString());
                }

                var existCartItem = CurrentCartItems.FirstOrDefault(c => c.Product.Id == product.Id);
                if (existCartItem is not null) { existCartItem.Quantity++; }
                else
                {
                    CurrentCartItems.Add(new CartItem { Cart = CurrentCart, Product = product, Quantity = 1, Selected = true });
                }
            }

            ViewData["Header"] = header;
        }

        public ActionResult OnPostUpdateCart([FromBody] CartResquestDto cartResquestDto)
        {
            cartResquestDto.CurrentCartJson = Regex.Replace(cartResquestDto.CurrentCartJson, "&quot;", "\"");
            cartResquestDto.CurrentCartItemsJson = Regex.Replace(cartResquestDto.CurrentCartItemsJson, "&quot;", "\"");
            cartResquestDto.CurrentHeaderJson = Regex.Replace(cartResquestDto.CurrentHeaderJson, "&quot;", "\"");
            CurrentCart = JsonConvert.DeserializeObject<Cart>(cartResquestDto.CurrentCartJson);
            CurrentCartItems = JsonConvert.DeserializeObject<List<CartItem>>(cartResquestDto.CurrentCartItemsJson);

            var header = JsonConvert.DeserializeObject<_HeaderModel.HeaderModel>(cartResquestDto.CurrentHeaderJson);
            var currentUser = userRepository.GetAll().First(user => user.Email == header.UserEmail);

            ViewData["Header"] = header;

            if (CurrentCart.CartTotal == 0 && cartResquestDto.CartDto.CartItemDtos.Length == 0)
            {
                return Page();
            }

            var updateCart = new Cart
            {
                Id = CurrentCart.Id,
                CartTotal = cartResquestDto.CartDto?.CartTotal ?? 0,
                UserCart = currentUser
            };

            UpdateNewCart(updateCart);

            var updateCartItems = new List<CartItem>();
            foreach (var currentCartItemDto in cartResquestDto.CartDto.CartItemDtos)
            {
                currentCartItemDto.ProductJson = Regex.Replace(currentCartItemDto.ProductJson, "&quot;", "\"");
                var currentProduct = JsonConvert.DeserializeObject<Product>(currentCartItemDto.ProductJson);
                if (currentProduct is not null)
                {
                    var currentCartItem = CurrentCartItems?.FirstOrDefault(c => c.Product.Id == currentProduct.Id);
                    if (currentCartItem is not null)
                    {
                        currentCartItem.Quantity = currentCartItemDto.Quantity;
                        updateCartItems.Add(currentCartItem);
                    }
                    else
                    {
                        updateCartItems.Add(new CartItem { Cart = updateCart, Product = currentProduct, Selected = true, Quantity = currentCartItemDto.Quantity });
                    }
                }
            }

            UpdateNewCartItem(updateCartItems, updateCart.Id.ToString());

            CurrentCart = updateCart;
            CurrentCartItems = updateCartItems;

            return RedirectToPage("/Cart", new { headerJson = cartResquestDto.CurrentHeaderJson });
        }


        private List<CartItem> GetCartItems(string cartId)
        {
            return cartItemRepository.GetAllByCartId(cartId);
        }

        private bool IsExistedCart(string cartId)
        {
            return cartRepository.GetById(cartId) is not null ? true : false;
        }

        private bool IsExistCartItem(CartItem cartItem)
        {
            var all = cartItemRepository.GetAll();
            var existCartItem = all.FirstOrDefault(c => c.Product.Id == cartItem.Product.Id && c.Cart.Id == cartItem.Cart.Id);
            if (existCartItem is not null)
            {
                if (existCartItem.Id != cartItem.Id)
                {
                    cartItem.Id = existCartItem.Id;
                }
                return true;
            }
            return false;
        }

        private void UpdateNewCartItem(List<CartItem> cartItems, string currentCartId)
        {
            var oldCartItems = cartItemRepository.GetAllByCartId(currentCartId);

            foreach (var oldCartItem in oldCartItems)
            {
                if (!cartItems.Any(c => c.Product.Id == oldCartItem.Product.Id))
                {
                    cartItemRepository.Delete(oldCartItem);
                }
            }

            foreach (var updateCartItem in cartItems)
            {
                if (IsExistCartItem(updateCartItem))
                {
                    cartItemRepository.Update(updateCartItem);
                }
                else
                {
                    cartItemRepository.Create(updateCartItem);
                }
            }
        }
        private void UpdateNewCart(Cart cart)
        {
            if (IsExistedCart(cart.Id.ToString()))
            {
                cartRepository.Update(cart);
            }
            else
            {
                cartRepository.Create(cart);
            }
        }
    }

    public class CartResquestDto
    {
        public CartDto CartDto { get; set; }
        public string CurrentCartJson { get; set; }
        public string CurrentCartItemsJson { get; set; }
        public string CurrentHeaderJson { get; set; }
    }
}
