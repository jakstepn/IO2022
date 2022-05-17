using ShopModule.Data;
using ShopModule.Products;
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
         List<Product> GetPaginatedProductList(int page, int pageSize);
         List<Product> GetPaginatedProductListFromCategory(int page, int pageSize, string category);
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
                _context.Remove(res);
                removed = _context.SaveChanges() == 1;
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
        /// Returns given number of elements (page) from the total
        /// </summary>
        /// <param name="page">Page index</param>
        /// <param name="pageSize">Number of elements on a page</param>
        /// <returns>List of products. If none were added returns an empty list</returns>
        public List<Product> GetPaginatedProductList(int page, int pageSize)
        {
            return _context.Products.Skip(page * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// Returns given number of elements from a given category in a paginated fashion
        /// </summary>
        /// <param name="page">Page index</param>
        /// <param name="pageSize">Number of elements on a page</param>
        /// <param name="category">Name of the category</param>
        /// <returns>Returns <= pageSize elements from a database. Returns null if category doesn't exist</returns>
        public List<Product> GetPaginatedProductListFromCategory(int page, int pageSize, string category)
        {
            var categoryList = _context.Products.Where(p => p.ProductType.Name == category);
            if (categoryList.Count() > 0)
            {
                return categoryList.Skip(page * pageSize).Take(pageSize).ToList();
            }
            else
            {
                return null;
            }
        }
    }
}
