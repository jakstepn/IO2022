using ShopModule.Data;
using ShopModule.Models;
using ShopModule.Products;
using ShopModule_ApiClasses.Messages;
using ShopModule_ApiClasses.Messages.Request;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopModule.Services
{
    public interface IProductService
    {
        Product RemoveProduct(Guid productId);
        Product FindProduct(Guid productId);
        Product AddProduct(Product product);
        Product UpdateProduct(Guid productId, RequestProductMessage product, ProductType category);
        List<ProductMessage> GetPaginatedProductList(int page, int pageSize);
        List<ProductMessage> GetPaginatedProductListFromCategory(int page, int pageSize, string category);
        ProductType GetOrCreateCategory(string name);
    }

    public class ProductService : IProductService
    {
        private readonly ShopModuleDbContext _context;

        public ProductService(ShopModuleDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Removes product from the database
        /// </summary>
        /// <param name="productId">Product id to be removed</param>
        /// <returns>Return found and removed product on success, null on failure</returns>
        public Product RemoveProduct(Guid productId)
        {
            var res = _context.Products.Find(productId);
            bool removed = false;
            if (res != null)
            {
                _context.OrderItems.RemoveRange(_context.OrderItems.Where(x => x.ProductFK == productId));
                _context.Remove(res);
                _context.SaveChanges();
                removed = true;
            }
            return removed ? res : null;
        }

        /// <summary>
        /// Find product in a database
        /// </summary>
        /// <param name="productId">Id of the element to be found</param>
        /// <returns>Returns the first matching product on success and a null on failure</returns>
        public Product FindProduct(Guid productId)
        {
            return _context.Products.Find(productId);
        }

        /// <summary>
        /// Adds product to the product database
        /// </summary>
        /// <param name="product">Item to be added</param>
        /// <returns>Returns given product on success and null on fail</returns>
        public Product AddProduct(Product product)
        {
            _context.Products.Add(product);
            return _context.SaveChanges() == 1 ? product : null;
        }

        /// <summary>
        /// Updates product to the product database
        /// </summary>
        /// <param name="product">Item to be updated</param>
        /// <param name="category">Item category</param>
        /// <returns>Returns given product on success and null on fail</returns>
        public Product UpdateProduct(Guid productId, RequestProductMessage product, ProductType category)
        {
            var p = _context.Products.Find(productId);
            p.Update(product, category);
            return _context.SaveChanges() == 1 ? p : null;
        }

        /// <summary>
        /// Returns given number of elements (page) from the total
        /// </summary>
        /// <param name="page">Page index</param>
        /// <param name="pageSize">Number of elements on a page</param>
        /// <returns>List of products. If none were added returns an empty list</returns>
        public List<ProductMessage> GetPaginatedProductList(int page, int pageSize)
        {
            List<ProductMessage> productMessages = new List<ProductMessage>();
            foreach (var item in _context.Products.Skip(page * pageSize).Take(pageSize).ToList())
            {
                productMessages.Add(item.Convert(StaticData.defaultConverter));
            }
            return productMessages;
        }

        /// <summary>
        /// Returns given number of elements from a given category in a paginated fashion
        /// </summary>
        /// <param name="page">Page index</param>
        /// <param name="pageSize">Number of elements on a page</param>
        /// <param name="category">Name of the category</param>
        /// <returns>Returns <= pageSize elements from a database. Returns null if category doesn't exist</returns>
        public List<ProductMessage> GetPaginatedProductListFromCategory(int page, int pageSize, string category)
        {
            var categoryList = _context.Products.Where(p => p.ProductTypeFK == category);
            if (categoryList.Count() > 0)
            {
                List<ProductMessage> productMessages = new List<ProductMessage>();
                foreach (var item in categoryList.Skip(page * pageSize).Take(pageSize).ToList())
                {
                    productMessages.Add(item.Convert(StaticData.defaultConverter));
                }
                return productMessages;
            }
            else
            {
                return null;
            }
        }

        public ProductType GetOrCreateCategory(string name)
        {
            var category = _context.ProductTypes.Find(name);
            if (category == null)
            {
                _context.ProductTypes.Add(category = new ProductType { Name = name });
                _context.SaveChanges();
            }
            return category;
        }
    }
}
