using FurnitureApp.Models;
using FurnitureApp;

namespace DataAccess.DAO;

public class OrderItemDAO
{
	private static OrderItemDAO instance = new();
	private static readonly object instanceLock = new();
	private OrderItemDAO() { }
	public static OrderItemDAO Instance
	{
		get
		{
			lock (instanceLock)
			{
				instance ??= new OrderItemDAO();
			}
			return instance;
		}
	}

	public List<OrderItem> GetAll()
	{
		List<OrderItem> listOrderItem = [];
		try
		{
			using AppDbContext appDbContext = new();
			listOrderItem = [.. appDbContext.OrderItems];
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}
		return listOrderItem;
	}

	public void Create(OrderItem orderItem)
	{
		try
		{
			using AppDbContext appDbContext = new();
			appDbContext.OrderItems.Add(orderItem);
			appDbContext.SaveChanges();
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}
	}
}
