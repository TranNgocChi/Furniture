using DataAccess.DAO;
using DataAccess.Repository.IRepository;
using FurnitureApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.CRepository
{
    public class CartRepository : ICartRepository
	{
        public void Create(Cart cart) => CartDAO.Instance.Create(cart);
        public void Delete(Cart cart) => CartDAO.Instance.Delete(cart);
        public void DeleteAll() => CartDAO.Instance.DeleteAll();
        public List<Cart> GetAll() => CartDAO.Instance.GetAll();
        public Cart? GetById(string id) => CartDAO.Instance.GetById(id);
        public void Update(Cart cart) => CartDAO.Instance.Update(cart);
    }
}
