using BusinessObjects;
using DataAccess;
using System.Collections.Generic;

namespace Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDAO _productDao;

        // Assuming ProductDAO and CategoryDAO have been refactored to be non-static
        public ProductRepository(ProductDAO productDao)
        {
            _productDao = productDao;
        }

        public void SaveProduct(Product p) => _productDao.SaveProduct(p);
        public Product GetProductById(int id) => _productDao.FindProductById(id);
        public void DeleteProduct(Product p) => _productDao.DeleteProduct(p);
        public void UpdateProduct(Product p) => _productDao.UpdateProduct(p);
        public List<Category> GetCategories() => CategoryDAO.GetCategoryList();
        public List<Product> GetProducts() => _productDao.GetProducts();
    }
}