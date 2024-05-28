using FurnitureApp;
using FurnitureApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
	public class ProductDAO
	{
		//Using Singleton Design Pattern
		private static ProductDAO instance = new();
		private static readonly object instanceLock = new();
		private ProductDAO() { }
		public static ProductDAO Instance
		{
			get
			{
				lock (instanceLock)
				{
                    instance ??= new ProductDAO();
				}
				return instance;	
			}
		}

        public List<Product> GetAll()
        {
            List<Product> listProduct = [];
            try
            {
                using AppDbContext appDbContext = new();
                listProduct = [.. appDbContext.Products];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listProduct;
        }

        public Product? GetById(string id)
        {
            try
            {
                using AppDbContext appDbContext = new();
                var productFound = appDbContext.Products.FirstOrDefault(p => p.Id.ToString() == id);
                if (productFound != null)
                {
                    return productFound;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Create(Product product)
        {
            try
            {
                using AppDbContext appDbContext = new();
                appDbContext.Products.Add(product);
                appDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(Product product)
        {
            try
            {
                using AppDbContext appDbContext = new();
                appDbContext.Entry<Product>(product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                appDbContext.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(Product product)
        {
            try
            {
                using AppDbContext appDbContext = new();

                //Delete Product in CartItem
                var cartItemContainProduct = appDbContext.CartItems.SingleOrDefault(c => c.Product.Id == product.Id);

                if (cartItemContainProduct != null)
                {
                    appDbContext.CartItems.Remove(cartItemContainProduct);
                }

                //Delete Product in OrderItem
                var orderItemContainProduct = appDbContext.OrderItems.SingleOrDefault(o => o.Product.Id == product.Id);
                if(orderItemContainProduct != null)
                {
                    appDbContext.OrderItems.Remove(orderItemContainProduct);
                }

                //Delete Product
                appDbContext.Products.Remove(product);

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
                appDbContext.Products.RemoveRange(GetAll());
                appDbContext.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
