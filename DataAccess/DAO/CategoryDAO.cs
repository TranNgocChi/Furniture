using FurnitureApp;
using FurnitureApp.Models;

namespace DataAccess.DAO;

public class CategoryDAO
{
    //Using Singleton Design Pattern
    private static CategoryDAO instance = new();
    private static readonly object instanceLock = new();
    private CategoryDAO() { }
    public static CategoryDAO Instance
    {
        get
        {
            lock (instanceLock)
            {
                instance ??= new CategoryDAO();
            }
            return instance;
        }
    }

    public List<Category> GetAll()
    {
        List<Category> listCategory = [];
        try
        {
            using AppDbContext appDbContext = new();
            listCategory = [.. appDbContext.Categories];
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return listCategory;
    }

    public Category? GetById(string id)
    {
        try
        {
            using AppDbContext appDbContext = new();
            var categoryFound = appDbContext.Categories.FirstOrDefault(c => c.Id.ToString() == id);
            if (categoryFound != null)
            {
                return categoryFound;
            }
            return null;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void Create(Category category)
    {
        try
        {
            using AppDbContext appDbContext = new();
            appDbContext.Categories.Add(category);
            appDbContext.SaveChanges();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void Update(Category category)
    {
        try
        {
            using AppDbContext appDbContext = new();
            appDbContext.Entry<Category>(category).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            appDbContext.SaveChanges();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void Delete(Category category)
    {
        try
        {
            using AppDbContext appDbContext = new();

            //Delete product
            var productsByCategory = appDbContext.Products.Where(p => p.Category.Id == category.Id).ToList();
            appDbContext.Products.RemoveRange(productsByCategory);

            //Delete Category
            appDbContext.Categories.Remove(category);

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
            appDbContext.Categories.RemoveRange(GetAll());
            appDbContext.SaveChanges();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
