using FurnitureApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
	public interface ICartRepository
	{
        public List<Cart> GetAll();
        public Cart? GetById(string id);
        public void Create(Cart cart);
        public void Update(Cart cart);
        public void Delete(Cart cart);
        public void DeleteAll();
    }
}
