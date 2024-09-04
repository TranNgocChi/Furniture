using FurnitureApp.Models;

namespace DataAccess.Repository.IRepository;

public interface ICartRepository
{
    public List<Cart> GetAll();
    public Cart? GetById(string id);
    public List<Cart> GetByUserId(string userId);
    public void Create(Cart cart);
    public void Update(Cart cart);
    public void Delete(Cart cart);
    public void DeleteAll();
}
