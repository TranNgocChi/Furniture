using FurnitureApp.Models;
using FurnitureApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class OrderDAO
    {
        //Using Singleton Design Pattern
        private static OrderDAO instance = new();
        private static readonly object instanceLock = new();
        private OrderDAO() { }
        public static OrderDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    instance ??= new OrderDAO();
                }
                return instance;
            }
        }
        
        public List<Order> GetAll()
        {
            List<Order> listOrder= [];
            try
            {
                using AppDbContext appDbContext = new();
                listOrder = [.. appDbContext.Orders];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listOrder;
        }

        public Order? GetById(string id)
        {
            try
            {
                using AppDbContext appDbContext = new();
                var orderFound = appDbContext.Orders.FirstOrDefault(p => p.Id.ToString() == id);
                if (orderFound != null)
                {
                    return orderFound;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Create(Order order)
        {
            try
            {
                using AppDbContext appDbContext = new();
                appDbContext.Orders.Add(order);
                appDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(Order order)
        {
            try
            {
                using AppDbContext appDbContext = new();
                appDbContext.Entry<Order>(order).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                appDbContext.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(Order order)
        {
            try
            {
                using AppDbContext appDbContext = new();
                
                //Delete Address
                var addressOrder = appDbContext.Addresses.FirstOrDefault(ad => ad.Id == order.OrderAddress.Id);
                if (addressOrder != null)
                {
                    appDbContext.Addresses.Remove(addressOrder);
                }

                //Delete Order Item
                appDbContext.OrderItems.RemoveRange(order.OrderItems);

                //Delete Order
                appDbContext.Orders.Remove(order);

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
                appDbContext.Orders.RemoveRange(GetAll());
                appDbContext.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
