using DataAccess.DAO;
using DataAccess.Repository.IRepository;
using FurnitureApp.Models;

namespace DataAccess.Repository.CRepository;

public class CartItemRepository : ICartItemRepository
{
    public void Create(CartItem cartItem) => CartItemDAO.Instance.Create(cartItem);

    public void Delete(CartItem cartItem) => CartItemDAO.Instance.Delete(cartItem);

    public void DeleteAllByCartId(string cartId) => CartItemDAO.Instance.DeleteAll(cartId);

    public List<CartItem> GetAllByCartId(string cartId) => CartItemDAO.Instance.GetByCartId(cartId);
    public List<CartItem> GetAll() => CartItemDAO.Instance.GetAll();

    public void Update(CartItem cartItem) => CartItemDAO.Instance.Update(cartItem);

    public CartItem? GetById(string cartItemId) => CartItemDAO.Instance.GetById(cartItemId);
}
