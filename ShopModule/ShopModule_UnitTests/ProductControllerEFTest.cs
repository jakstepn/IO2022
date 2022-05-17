using Microsoft.EntityFrameworkCore;
using Moq;
using ShopModule.Data;
using ShopModule.Location;
using ShopModule.Orders;
using ShopModule.Products;
using ShopModule.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ShopModule_UnitTests
{
    public class ProductControllerEFTest
    {
        [Fact]
        public void GetProductsTest()
        {
            var mockOrderSet = new Mock<DbSet<Product>>();

            var mockContext = new Mock<ShopModuleDbContext>();
            mockContext.Setup(x => x.Products).Returns(mockOrderSet.Object);

            var service = new ProductService(mockContext.Object);

            var testProduct = new Product
            {
                Id = Guid.NewGuid(),
                Available = true,
                Price = 10,
                ProductName = "bulbulator",
                ProductTypeFK = "1",
                TaxRate = 10
            };

            service.AddProduct(testProduct);
            var products = service.GetPaginatedProductList(0, 1);
            Assert.Contains(testProduct, products);
        }

        [Fact]
        public void AddProductTest()
        {
            var mockOrderSet = new Mock<DbSet<Product>>();

            var mockContext = new Mock<ShopModuleDbContext>();
            mockContext.Setup(x => x.Products).Returns(mockOrderSet.Object);

            var service = new ProductService(mockContext.Object);

            var testProduct = new Product
            {
                Id = Guid.NewGuid(),
                Available = true,
                Price = 10,
                ProductName = "bulbulator",
                ProductTypeFK = "1",
                TaxRate = 10
            };

            var product = service.AddProduct(testProduct);
            mockOrderSet.Verify(m => m.Add(It.IsAny<Product>()), Times.Once);
        }

        [Fact]
        public void DeleteProductTest()
        {
            var mockOrderSet = new Mock<DbSet<Product>>();

            var mockContext = new Mock<ShopModuleDbContext>();
            mockContext.Setup(x => x.Products).Returns(mockOrderSet.Object);

            var service = new ProductService(mockContext.Object);

            var testProduct = new Product
            {
                Id = Guid.NewGuid(),
                Available = true,
                Price = 10,
                ProductName = "bulbulator",
                ProductTypeFK = "1",
                TaxRate = 10
            };

            service.AddProduct(testProduct);
            service.RemoveProduct(testProduct.Id);
            mockOrderSet.Verify(m => m.Remove(It.IsAny<Product>()), Times.Once);
        }

        [Fact]
        public void GetProductInfoTest()
        {
            var mockOrderSet = new Mock<DbSet<Product>>();

            var mockContext = new Mock<ShopModuleDbContext>();
            mockContext.Setup(x => x.Products).Returns(mockOrderSet.Object);

            var service = new ProductService(mockContext.Object);

            var testProduct = new Product
            {
                Id = Guid.NewGuid(),
                Available = true,
                Price = 10,
                ProductName = "bulbulator",
                ProductTypeFK = "1",
                TaxRate = 10
            };

            service.AddProduct(testProduct);
            var product = service.FindProduct(testProduct.Id);
            Assert.Equal(testProduct, product);
        }

        [Fact]
        public void GetProductsFromCategoryTest()
        {
            var mockOrderSet = new Mock<DbSet<Product>>();

            var mockContext = new Mock<ShopModuleDbContext>();
            mockContext.Setup(x => x.Products).Returns(mockOrderSet.Object);

            var service = new ProductService(mockContext.Object);

            var testCategory1 = new ProductType { Name="test1"};
            var testCategory2 = new ProductType { Name="test2"};


            var testProduct1 = new Product
            {
                Id = Guid.NewGuid(),
                Available = true,
                Price = 10,
                ProductName = "bulbulator1",
                ProductTypeFK = "1",
                TaxRate = 10
            };

            var testProduct2 = new Product
            {
                Id = Guid.NewGuid(),
                Available = true,
                Price = 10,
                ProductName = "bulbulator2",
                ProductTypeFK = "2",
                TaxRate = 10
            };

            service.AddProduct(testProduct1);
            service.AddProduct(testProduct2);
            var product = service.GetPaginatedProductListFromCategory(0,1,testCategory1.Name);
            Assert.Contains(testProduct1, product);
        }
    }
}
