using DataAccess.DAO;
using DataAccess.Repository.IRepository;
using FurnitureApp.Models;

namespace DataAccess.Repository.CRepository;

public class OrderItemRepository : IOrderItemRepository
{
	public void Create(OrderItem orderItem) => OrderItemDAO.Instance.Create(orderItem);
}
