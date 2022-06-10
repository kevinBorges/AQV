using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AntesQueVenca.Data.Context;
using AntesQueVenca.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AntesQueVenca.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        AntesQueVencaContext _aqvContext = new AntesQueVencaContext();

        [HttpPost]
        [AllowAnonymous]
        public void PostProduct(Product product)
        {
            _aqvContext.Products.Add(product);
            _aqvContext.SaveChanges();
        }

        [HttpGet]
        [AllowAnonymous]
        public Product GetProduct(int id)
        {
            var selectedProduct = _aqvContext.Products.Find(id);

            return selectedProduct;
        }

        [HttpGet]
        [AllowAnonymous]
        public IList<Product> GetAllProduct()
        {
            var productList = _aqvContext.Products.Where(p => !p.Deleted).ToList();

            return productList;
        }

        [HttpPut]
        [AllowAnonymous]
        public void UpdateProduct(Product product)
        {
            _aqvContext.Products.Update(product);
            _aqvContext.SaveChanges();
        }

        [HttpDelete]
        [AllowAnonymous]
        public void DeleteProduct(int id)
        {
            var selectedProduct = _aqvContext.Products.Find(id);
            _aqvContext.Products.Remove(selectedProduct);
            _aqvContext.SaveChanges();
        }

    }
}
