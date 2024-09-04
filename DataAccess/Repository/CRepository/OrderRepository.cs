using DataAccess.DAO;
using DataAccess.Repository.IRepository;
using FurnitureApp.Models;

namespace DataAccess.Repository.CRepository;

public class OrderRepository : IOrderRepository
{
    public void Create(Order order) => OrderDAO.Instance.Create(order);
    public void Delete(Order order) => OrderDAO.Instance.Delete(order);
    public void DeleteAll() => OrderDAO.Instance.DeleteAll();
    public List<Order> GetAll() => OrderDAO.Instance.GetAll();
    public Order? GetById(string id) => OrderDAO.Instance.GetById(id);
    public void Update(Order order) => OrderDAO.Instance.Update(order);
}
