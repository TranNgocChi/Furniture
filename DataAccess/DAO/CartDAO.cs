using FurnitureApp;
using FurnitureApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAO
{
    public class CartDAO
    {
        //Using Singleton Design Pattern
        private static CartDAO instance = new();
        private static readonly object instanceLock = new();
        private CartDAO() { }
        public static CartDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    instance ??= new CartDAO();
                }
                return instance;
            }
        }

        public List<Cart> GetAll()
        {
            List<Cart> listCart = [];
            try
            {
                using AppDbContext appDbContext = new();
                listCart = [.. appDbContext.Carts.Include(cart => cart.UserCart)];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listCart;
        }

        public Cart? GetById(string id)
        {
            try
            {
                using AppDbContext appDbContext = new();
                var cartFound = appDbContext.Carts
                    .Include(cart => cart.UserCart)
                    .FirstOrDefault(c => c.Id.ToString() == id);
                if (cartFound != null)
                {
                    return cartFound;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Cart> GetByUserId(string userId)
        {
            List<Cart> cartsFound = [];
            try
            {
                using AppDbContext appDbContext = new();
                cartsFound = [..appDbContext.Carts
                    .Include(cart => cart.UserCart)
                    .Where(c => c.UserCart.Id.ToString() == userId)];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return cartsFound;
        }

        public void Create(Cart cart)
        {
            try
            {
                using AppDbContext appDbContext = new();
                cart = TrackCart(cart, appDbContext);
                appDbContext.Carts.Add(cart);
                appDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(Cart cart)
        {
            try
            {
                using AppDbContext appDbContext = new();
                cart = TrackCart(cart, appDbContext);
                appDbContext.Entry<Cart>(cart).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                appDbContext.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(Cart cart)
        {
            try
            {
                using AppDbContext appDbContext = new();
                cart = TrackCart(cart, appDbContext);
                appDbContext.Carts.Remove(cart);
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
                appDbContext.Carts.RemoveRange(GetAll());
                appDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private Cart TrackCart(Cart cart, AppDbContext appDbContext)
        {
            try
            {
                appDbContext.Entry(cart.UserCart).State = EntityState.Detached;

                var currentUser = appDbContext.Users.FirstOrDefault(u => u.Id == cart.UserCart.Id);
                if (currentUser is not null)
                {
                    cart.UserCart = currentUser;
                }
                return cart;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
