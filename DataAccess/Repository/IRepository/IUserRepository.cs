using FurnitureApp.Models;

namespace DataAccess.Repository.IRepository;

public interface IUserRepository
{
    public List<User> GetAll();
    public User? GetById(string id);
    public User? GetByEmail(string email);
    public void Create(User user);
    public void Update(User user);
    public void Delete(User user);
    public void DeleteAll();

}
