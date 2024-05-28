using FurnitureApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
	public interface ICategoryRepository
	{
        public List<Category> GetAll();
        public Category? GetById(string id);
        public void Create(Category category);
        public void Update(Category category);
        public void Delete(Category category);
        public void DeleteAll();
    }
}
