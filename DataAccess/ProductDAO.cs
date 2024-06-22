using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace DataAccess
{
    public class ProductDAO
    {
        private MyDbContext _context;

        public ProductDAO(MyDbContext context)
        {
            _context = context;
        }

        public List<Product> GetProducts()
        {
            var listProducts = new List<Product>();
            try
            {

                listProducts = _context.Products.ToList();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listProducts;
        }

        public Product FindProductById(int proId)
        {
            Product p = new Product();
            try
            {

                p = _context.Products.SingleOrDefault(x => x.ProductId == proId);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return p;
        }

        public void SaveProduct(Product p)
        {
            try
            {

                _context.Products.Add(p);
                _context.SaveChanges();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void UpdateProduct(Product p)
        {
            try
            {
                _context.Entry<Product>(p).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void DeleteProduct(Product p)
        {
            try
            {

                    var p1 = _context.Products.SingleOrDefault(c => c.ProductId == p.ProductId);
                    _context.Products.Remove(p1);
                    _context.SaveChanges();
                
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
