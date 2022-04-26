using ShopModule;
using ShopModule.Employees;
using ShopModule.Location;
using ShopModule.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ShopModule_UnitTests
{
    public class UnitTestShopManagerClass
    {
        [Fact]
        public void TestAddProduct()
        {
            Shop s = new Shop
            {
                Address = new Address
                {
                    City = "nocity",
                    Country = "nocountry",
                    Region = "noregion",
                    Street = "nostreet",
                    StreetNumber = "0",
                    ZipCode = "nozipcode"
                },
                Email = "noemail",
                Id = "firstshop",
                PhoneNumber = "none",
            };
            ShopManager sm = new ShopManager();
            Product p = new Product
            {
                Id = "test",
                Available = true,
                Price = 10,
                ProductName = "testname",
                ShopFK = "0",
                TaxRate = 1
            };
            sm.AddProduct(s, p);

            // Check if product exists
            bool exists = s.Products.Contains(p);
            Assert.True(exists);
        }
        [Fact]
        public void TestDeleteProduct()
        {
            Shop s = new Shop
            {
                Address = new Address
                {
                    City = "nocity",
                    Country = "nocountry",
                    Region = "noregion",
                    Street = "nostreet",
                    StreetNumber = "0",
                    ZipCode = "nozipcode"
                },
                Email = "noemail",
                Id = "firstshop",
                PhoneNumber = "none",
            };
            ShopManager sm = new ShopManager();
            Product p = new Product
            {
                Id = "test",
                Available = true,
                Price = 10,
                ProductName = "testname",
                ShopFK = "0",
                TaxRate = 1
            };
            sm.AddProduct(s, p);
            sm.DeleteProduct(s, p);

            // Check if product exists
            bool exists = s.Products.Contains(p);
            Assert.False(exists);
        }
        [Fact]
        public void TestAddShop()
        {
            ShopManager sm = new ShopManager();
        }
        [Fact]
        public void TestDeleteShop()
        {

        }
        [Fact]
        public void TestGetShopHistory()
        {

        }
    }
}
