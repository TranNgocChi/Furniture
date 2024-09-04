using DataAccess.DAO;
using DataAccess.Repository.IRepository;
using FurnitureApp.Models;

namespace DataAccess.Repository.CRepository;

public class ProductRepository : IProductRepository
{
    public void Create(Product product) => ProductDAO.Instance.Create(product);
    public void Delete(Product product) => ProductDAO.Instance.Delete(product);
    public void DeleteAll() => ProductDAO.Instance.DeleteAll();
    public List<Product> GetAll() => ProductDAO.Instance.GetAll();
    public Product? GetById(string id) => ProductDAO.Instance.GetById(id);
    public void Update(Product product) => ProductDAO.Instance.Update(product);
}
