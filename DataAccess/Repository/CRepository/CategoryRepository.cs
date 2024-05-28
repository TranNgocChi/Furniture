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
    public class CategoryRepository : ICategoryRepository
	{
        public void Create(Category category) => CategoryDAO.Instance.Create(category);
        public void Delete(Category category) => CategoryDAO.Instance.Delete(category);
        public void DeleteAll() => CategoryDAO.Instance.DeleteAll();
        public List<Category> GetAll() => CategoryDAO.Instance.GetAll();
        public Category? GetById(string id) => CategoryDAO.Instance.GetById(id);
        public void Update(Category category) => CategoryDAO.Instance.Update(category);
    }
}
