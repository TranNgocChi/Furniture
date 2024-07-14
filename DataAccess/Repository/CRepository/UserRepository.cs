using DataAccess.DAO;
using DataAccess.Repository.IRepository;
using FurnitureApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.CRepository
{
	public class UserRepository : IUserRepository
	{
		public void Create(User user) => UserDAO.Instance.Create(user);
		public void Delete(User user) => UserDAO.Instance.Delete(user);
		public void DeleteAll() => UserDAO.Instance.DeleteAll();
		public List<User> GetAll() => UserDAO.Instance.GetAll();
		public User? GetById(string id) => UserDAO.Instance.GetById(id);
		public void Update(User user) => UserDAO.Instance.Update(user);

	}
}
