using FurnitureApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
	public interface IProductRepository
	{
        public List<Product> GetAll();
        public Product? GetById(string id);
        public void Create(Product product);
        public void Update(Product product);
        public void Delete(Product product);
        public void DeleteAll();
    }
}
