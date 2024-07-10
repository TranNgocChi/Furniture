using FurnitureApp;
using FurnitureApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAO
{
    public class CartItemDAO
    {
        //Using Singleton Design Pattern
        private static CartItemDAO instance = new();
        private static readonly object instanceLock = new();
        private CartItemDAO() { }
        public static CartItemDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    instance ??= new CartItemDAO();
                }
                return instance;
            }
        }

        public List<CartItem> GetAll()
        {
            var cartItems = new List<CartItem>();
            try
            {
                using AppDbContext appDbContext = new();
                cartItems = [.. appDbContext.CartItems.Include(cartItem => cartItem.Product).Include(cartItem => cartItem.Cart)];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return cartItems;
        }

        public CartItem? GetById(string id)
        {
            try
            {
                using AppDbContext appDbContext = new();
                var cartItem = appDbContext.CartItems
                    .Include(cartItem => cartItem.Product)
                    .Include(cartItem => cartItem.Cart)
                    .FirstOrDefault(c => c.Id.ToString() == id);
                if (cartItem != null)
                {
                    return cartItem;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<CartItem> GetByCartId(string cartId)
        {
            var cartItems = new List<CartItem>();
            try
            {
                using AppDbContext appDbContext = new();
                cartItems = GetAll().FindAll(c => c.Cart.Id.ToString() == cartId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return cartItems;
        }

        public void Create(CartItem cartItem)
        {

            try
            {
                using AppDbContext appDbContext = new();
                cartItem = TrackCartItem(cartItem, appDbContext);
                appDbContext.CartItems.Add(cartItem);
                appDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(CartItem cartItem)
        {
            try
            {
                using AppDbContext appDbContext = new();
                var existCartItem = GetById(cartItem.Id.ToString());
                if (existCartItem is not null)
                {
                    existCartItem.Quantity = cartItem.Quantity;
                    appDbContext.Entry<CartItem>(existCartItem).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    appDbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(CartItem cartItem)
        {
            try
            {
                using AppDbContext appDbContext = new();
                cartItem = TrackCartItem(cartItem, appDbContext);
                appDbContext.CartItems.Remove(cartItem);
                appDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteAll()
        {
            try
            {
                using AppDbContext appDbContext = new();
                appDbContext.CartItems.RemoveRange(GetAll());
                appDbContext.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteAll(string cartId)
        {
            try
            {
                using AppDbContext appDbContext = new();
                var cartItems = GetByCartId(cartId);
                if (cartItems is not null) appDbContext.CartItems.RemoveRange(cartItems);
                appDbContext.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private CartItem TrackCartItem(CartItem cartItem, AppDbContext appDbContext)
        {
            try
            {
                appDbContext.Set<Product>().AsNoTracking();
                appDbContext.Set<Cart>().AsNoTracking();

                var currentCart = appDbContext.Carts.FirstOrDefault(c => c.Id == cartItem.Cart.Id);
                var currentProduct = appDbContext.Products.FirstOrDefault(p => p.Id == cartItem.Product.Id);

                if (currentCart is not null && currentProduct is not null)
                {
                    cartItem.Cart = currentCart;
                    cartItem.Product = currentProduct;
                }
                return cartItem;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
