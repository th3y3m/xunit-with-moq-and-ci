using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace UnitTests
{
    public class ProductDAOTests
    {
        private readonly Mock<DbSet<Product>> mockSet;
        private readonly Mock<MyDbContext> mockContext;
        private readonly List<Product> productList;

        public ProductDAOTests()
        {
            // Initialize mock set and context
            mockSet = new Mock<DbSet<Product>>();
            mockContext = new Mock<MyDbContext>();
            productList = new List<Product>
            {
                new Product { ProductId = 1, ProductName = "Test Product 1" },
                new Product { ProductId = 2, ProductName = "Test Product 2" }
            };

            var data = productList.AsQueryable();

            mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            mockContext.Setup(c => c.Products).Returns(mockSet.Object);
        }

        [Fact]
        public void GetProducts_ReturnsAllProducts()
        {
            // Arrange
            var dao = new ProductDAO(mockContext.Object);

            // Act
            var result = dao.GetProducts();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal(productList, result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void FindProductById_ReturnsCorrectProduct(int productId)
        {
            // Arrange
            var dao = new ProductDAO(mockContext.Object);

            // Act
            var result = dao.FindProductById(productId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(productId, result.ProductId);
        }

        [Fact]
        public void SaveProduct_AddsProduct()
        {
            var product = new Product { ProductId = 3, ProductName = "Test Product 3" };
            var dao = new ProductDAO(mockContext.Object);

            dao.SaveProduct(product);

            mockSet.Verify(m => m.Add(It.IsAny<Product>()), Times.Once);
            mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }

        [Fact]
        public void UpdateProduct_UpdatesProduct()
        {
            var product = new Product { ProductId = 1, ProductName = "Updated Product" };
            var dao = new ProductDAO(mockContext.Object);

            dao.UpdateProduct(product);

            mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }

        [Fact]
        public void DeleteProduct_DeletesProduct()
        {
            var product = new Product { ProductId = 2, ProductName = "Test Product 2" };
            var dao = new ProductDAO(mockContext.Object);

            dao.DeleteProduct(product);

            mockSet.Verify(m => m.Remove(It.IsAny<Product>()), Times.Once);
            mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }
    }
}
