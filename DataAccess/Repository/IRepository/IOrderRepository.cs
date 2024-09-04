using FurnitureApp.Models;

namespace DataAccess.Repository.IRepository;

public interface IOrderRepository
{
    public List<Order> GetAll();
    public Order? GetById(string id);
    public void Create(Order order);
    public void Update(Order order);
    public void Delete(Order order);
    public void DeleteAll();
}
