using DataAccess.DAO;
using DataAccess.Repository.IRepository;
using FurnitureApp.Models;

namespace DataAccess.Repository.CRepository
{
    public class CartRepository : ICartRepository
    {
        public void Create(Cart cart) => CartDAO.Instance.Create(cart);
        public void Delete(Cart cart) => CartDAO.Instance.Delete(cart);
        public void DeleteAll() => CartDAO.Instance.DeleteAll();
        public List<Cart> GetAll() => CartDAO.Instance.GetAll();
        public Cart? GetById(string id) => CartDAO.Instance.GetById(id);

        public List<Cart> GetByUserId(string userId) => CartDAO.Instance.GetByUserId(userId);

        public void Update(Cart cart) => CartDAO.Instance.Update(cart);
    }
}
