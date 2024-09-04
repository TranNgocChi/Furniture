using FurnitureApp.Models;

namespace DataAccess.Repository.IRepository;

public interface ICategoryRepository
{
    public List<Category> GetAll();
    public Category? GetById(string id);
    public void Create(Category category);
    public void Update(Category category);
    public void Delete(Category category);
    public void DeleteAll();
}
