using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using Repositories;

namespace ProductManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
	    private IProductRepository repository;

        public ProductsController(IProductRepository ip)
        {
            this.repository = ip;
        }

        // GET: api/Products
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProduct() => repository.GetProducts();

        // POST: ProductsController/Products
        [HttpPost]
        public IActionResult PostProduct(Product p)
        {
            repository.SaveProduct(p);
            return NoContent();
        }

		// GET: ProductsController/Delete/5
		[HttpDelete("{id}")]
		public IActionResult DeleteProduct(int id)
		{
            var p = repository.GetProductById(id);
            if (p == null)
            {
	            return NotFound();
            }
            repository.DeleteProduct(p);
            return NoContent();
		}

		[HttpPut("id")]
		public IActionResult UpdateProduct(int id, Product p)
		{
            var pImp = repository.GetProductById(id);
            if (pImp == null)
            {
	            return NotFound();
            }
            repository.UpdateProduct(p);
            return NoContent();
		}
    }
}
