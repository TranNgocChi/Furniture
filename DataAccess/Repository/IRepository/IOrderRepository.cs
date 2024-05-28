using FurnitureApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
    public interface IOrderRepository
    {
        public List<Order> GetAll();
        public Order? GetById(string id);
        public void Create(Order order);
        public void Update(Order order);
        public void Delete(Order order);
        public void DeleteAll();
    }
}
