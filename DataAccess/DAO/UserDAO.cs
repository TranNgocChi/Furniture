using FurnitureApp;
using FurnitureApp.Models;

namespace DataAccess.DAO;

public class UserDAO
{
    //Using Singleton Design Pattern
    private static UserDAO instance = new();
    private static readonly object instanceLock = new();
    private UserDAO() { }
    public static UserDAO Instance
    {
        get
        {
            lock (instanceLock)
            {
                instance ??= new UserDAO();
            }
            return instance;
        }
    }
    public List<User> GetAll()
    {
        List<User> listUser = [];
        try
        {
            using AppDbContext appDbContext = new();
            listUser = [.. appDbContext.Users];
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return listUser;
    }

    public User? GetById(string id)
    {
        try
        {
            using AppDbContext appDbContext = new();
            var userFound = appDbContext.Users.FirstOrDefault(u => u.Id == id);
            if (userFound != null)
            {
                return userFound;
            }
            return null;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public User? GetByEmail(string? email)
    {
        try
        {
            using AppDbContext appDbContext = new();
            var userFound = appDbContext.Users.FirstOrDefault(u => u.Email == email);
            if (userFound != null)
            {
                return userFound;
            }
            return null;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void Create(User user)
    {
        try
        {
            using AppDbContext appDbContext = new();
            appDbContext.Users.Add(user);
            appDbContext.SaveChanges();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void Update(User user)
    {
        try
        {
            using AppDbContext appDbContext = new();
            appDbContext.Entry<User>(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            appDbContext.SaveChanges();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    
    public void Delete(User user)
    {
        try
        {
            using AppDbContext appDbContext = new();

            //Delete Cart of User
            var userCart = appDbContext.Carts.SingleOrDefault(c => c.UserCart.Id == user.Id);
            if (userCart != null)
            {
                appDbContext.Carts.Remove(userCart);
            }

            //Delete Order of User
            var userOrder = appDbContext.Orders.SingleOrDefault(c => c.UserOrder.Id == user.Id);
            if (userOrder != null)
            {
                appDbContext.Orders.Remove(userOrder);
            }

            //Delete User
            appDbContext.Users.Remove(user);

            appDbContext.SaveChanges();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void DeleteAll()
    {
        try
        {
            using AppDbContext appDbContext = new();
            appDbContext.Users.RemoveRange(GetAll());
            appDbContext.SaveChanges();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

}
