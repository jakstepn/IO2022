using System;
using ShopModule.Orders;
using ShopModule.Products;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopModule.Employees
{
	public class ShopManager : ShopEmployee
	{

		public ShopManager() : base()
		{
		}

		public void AddProduct(Shop shop, Product product) { shop.Products.Add(product); }
		public void DeleteProduct(Shop shop, Product product) { shop.Products.Remove(product); }
		public void AddShop(Shop shop, Product product) { shop.Products.Add(product); }
		public void DeleteShop(Shop shop) { }
		public List<Order> GetOrdersHistory(Shop shop) 
		{ 
			List<Order> finished = new List<Order>();
            foreach (var item in shop.Orders)
            {
				if(item.OrderStatus == OrderStatus.Delivered)
                {
					finished.Add(item);
                }
            }
			return finished;
		}
	}
}
