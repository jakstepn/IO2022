using Microsoft.EntityFrameworkCore;
using Moq;
using ShopModule.Data;
using ShopModule.Location;
using ShopModule.Models;
using ShopModule.Orders;
using ShopModule.Products;
using ShopModule.Services;
using ShopModule_ApiClasses.Messages;
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
            var mockService = new Mock<IProductService>();

            var mockContext = new Mock<ShopModuleDbContext>();
            mockContext.Setup(x => x.Products).Returns(mockOrderSet.Object);

            var service = new ProductService(mockContext.Object);

            var productCategory = new ProductType { Name = "testcategory" };

            var testProduct = new Product
            {
                Available = true,
                Price = 10,
                ProductName = "bulbulator",
                ProductType = productCategory,
                TaxRate = 10
            };

            mockService.Setup(x => x.GetPaginatedProductList(0,1))
                .Returns(new List<ProductMessage> { testProduct.Convert(StaticData.defaultConverter) });

            service.AddProduct(testProduct);
            var products = mockService.Object.GetPaginatedProductList(0, 1);
            Assert.Equal(testProduct.Convert(StaticData.defaultConverter).name, products.ElementAt(0).name);
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
            var mockService = new Mock<IProductService>();

            var mockContext = new Mock<ShopModuleDbContext>();
            mockContext.Setup(x => x.Products).Returns(mockOrderSet.Object);

            var service = new ProductService(mockContext.Object);

            var testProduct = new Product
            {
                Available = true,
                Price = 10,
                ProductName = "bulbulator",
                ProductTypeFK = "1",
                TaxRate = 10
            };

            mockService.Setup(x => x.RemoveProduct(testProduct.ProductName)).Returns(testProduct);
            mockService.Setup(x => x.AddProduct(testProduct)).Returns(testProduct);

            mockService.Object.AddProduct(testProduct);
            mockService.Object.RemoveProduct(testProduct.ProductName);
            mockOrderSet.Verify(m => m.Remove(It.IsAny<Product>()), Times.AtLeastOnce);
        }

        [Fact]
        public void GetProductInfoTest()
        {
            var mockOrderSet = new Mock<DbSet<Product>>();
            var mockService = new Mock<IProductService>();

            var mockContext = new Mock<ShopModuleDbContext>();
            mockContext.Setup(x => x.Products).Returns(mockOrderSet.Object);

            var service = new ProductService(mockContext.Object);

            var testProduct = new Product
            {
                Available = true,
                Price = 10,
                ProductName = "bulbulator",
                ProductTypeFK = "1",
                TaxRate = 10
            };

            service.AddProduct(testProduct);
            mockService.Setup(x => x.FindProduct(testProduct.ProductName)).Returns(testProduct);
            var product = mockService.Object.FindProduct(testProduct.ProductName);
            Assert.Equal(testProduct, product);
        }

        [Fact]
        public void GetProductsFromCategoryTest()
        {
            var mockOrderSet = new Mock<DbSet<Product>>();
            var mockService = new Mock<IProductService>();

            var mockContext = new Mock<ShopModuleDbContext>();
            mockContext.Setup(x => x.Products).Returns(mockOrderSet.Object);

            var service = new ProductService(mockContext.Object);

            var testCategory1 = new ProductType { Name="test1"};
            var testCategory2 = new ProductType { Name="test2"};

            var testProduct1 = new Product
            {
                Available = true,
                Price = 10,
                ProductName = "bulbulator1",
                ProductType = testCategory1,
                TaxRate = 10
            };

            var testProduct2 = new Product
            {
                Available = true,
                Price = 10,
                ProductName = "bulbulator2",
                ProductType = testCategory2,
                TaxRate = 10
            };

            mockService.Setup(x => x.GetPaginatedProductListFromCategory(0, 1, testCategory1.Name))
                .Returns(new List<ProductMessage> { testProduct1.Convert(StaticData.defaultConverter) });

            service.AddProduct(testProduct1);
            service.AddProduct(testProduct2);
            var product = mockService.Object.GetPaginatedProductListFromCategory(0,1,testCategory1.Name);
            Assert.Equal(testProduct1.Convert(StaticData.defaultConverter).name, product.ElementAt(0).name);
        }
    }
}
