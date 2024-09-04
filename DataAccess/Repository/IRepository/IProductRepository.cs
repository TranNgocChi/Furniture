using FurnitureApp.Models;

namespace DataAccess.Repository.IRepository;

public interface IProductRepository
{
    public List<Product> GetAll();
    public Product? GetById(string id);
    public void Create(Product product);
    public void Update(Product product);
    public void Delete(Product product);
    public void DeleteAll();
}
