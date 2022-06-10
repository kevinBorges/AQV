using AntesQueVenca.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AntesQueVenca.Data.Repositories
{
    public class ProductItemRepository:RepositoryBase<ProductItem>
    {
        public override IEnumerable<ProductItem> GetAll()
        {
            return Db.ProductItem.Include(prop => prop.Product);
        }
    }
}
