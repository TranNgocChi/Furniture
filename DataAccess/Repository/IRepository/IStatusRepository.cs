using FurnitureApp.Models;

namespace DataAccess.Repository.IRepository;

public interface IStatusRepository
{
	public List<Status> GetAll();
}
