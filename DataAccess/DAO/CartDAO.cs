using FurnitureApp.Models;
using FurnitureApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                listCart = [.. appDbContext.Carts];
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
                var cartFound = appDbContext.Carts.FirstOrDefault(c => c.Id.ToString() == id);
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

        public void Create(Cart cart)
        {
            try
            {
                using AppDbContext appDbContext = new();
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
                appDbContext.CartItems.RemoveRange(cart.CartItems);
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


    }
}
