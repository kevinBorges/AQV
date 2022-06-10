using AntesQueVenca.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AntesQueVenca.Data.Repositories
{
    public class RetailerProductItemRepository : RepositoryBase<RetailerProductItem>
    {
        public override IEnumerable<RetailerProductItem> GetAll()
        {
            return Db.RetailerProductItem.Include(prop => prop.ProductItem).ThenInclude(prop => prop.Product).ThenInclude(prop => prop.Category).Where(p=>p.ProductItem.ExpirationDate > DateTime.Now);
        }
    }
}
