using FurnitureApp.Models;

namespace DataAccess.Repository.IRepository;

public interface ICartItemRepository
{
    public List<CartItem> GetAllByCartId(string cartId);
    public List<CartItem> GetAll();
    public CartItem? GetById(string cartItemId);
    public void Update(CartItem cartItem);
    public void Create(CartItem cartItem);
    public void Delete(CartItem cartItem);
    public void DeleteAllByCartId(string cartId);
}
