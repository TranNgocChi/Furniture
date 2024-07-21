using FurnitureApp;
using FurnitureApp.Models;

namespace DataAccess.DAO;

public class StatusDAO
{
	//Using Singleton Design Pattern
	private static StatusDAO instance = new();
	private static readonly object instanceLock = new();
	private StatusDAO() { }
	public static StatusDAO Instance
	{
		get
		{
			lock (instanceLock)
			{
				instance ??= new StatusDAO();
			}
			return instance;
		}
	}

	public List<Status> GetAll()
	{
		List<Status> listStatus = [];
		try
		{
			using AppDbContext appDbContext = new();
			listStatus = [.. appDbContext.Statuses];
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}
		return listStatus;
	}

	public void Create(Status status)
	{
		try
		{
			using AppDbContext appDbContext = new();
			appDbContext.Statuses.Add(status);
			appDbContext.SaveChanges();
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}
	}

	public void Update(Status status)
	{
		try
		{
			using AppDbContext appDbContext = new();
			appDbContext.Entry<Status>(status).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
			appDbContext.SaveChanges();

		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}
	}
}
