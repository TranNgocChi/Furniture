using DataAccess.DAO;
using DataAccess.Repository.IRepository;
using FurnitureApp.Models;

namespace DataAccess.Repository.CRepository;

public class StatusRepository : IStatusRepository
{
	public List<Status> GetAll() => StatusDAO.Instance.GetAll();
}
