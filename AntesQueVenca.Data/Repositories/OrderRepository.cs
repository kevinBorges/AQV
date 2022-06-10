using AntesQueVenca.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AntesQueVenca.Data.Repositories
{
    public class OrderRepository : RepositoryBase<Order>
    {
        public override IEnumerable<Order> GetAll()
        {
            return Db.Orders.Include(l => l.Consumer)
                .Include(l => l.Consumer.Person).ThenInclude(l => l.Addresses)
                .Include(l => l.Consumer.Person)
                .Include(prop => prop.OrderProducts).ThenInclude(prop => prop.ProductItem).ThenInclude(prop => prop.Product)
                .Where(l=>!l.Deleted);
        }

        public override Order GetById(int id)
        {
            return Db.Orders.Include(l => l.Consumer)
                .Include(l => l.Consumer.Person).ThenInclude(l=>l.Addresses)
                .Include(l => l.Consumer.Person).ThenInclude(p=>p.Phones)
                .Include(prop => prop.OrderProducts).ThenInclude(prop => prop.ProductItem).ThenInclude(prop => prop.Product)
                .Where(l => l.OrderId == id).FirstOrDefault();
        }
    }
}
