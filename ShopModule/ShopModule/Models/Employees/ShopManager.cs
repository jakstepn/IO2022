using System;
using ShopModule.Orders;
using ShopModule.Products;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopModule.Employees
{
	[Table("ShopManagers")]
	public class ShopManager : ShopEmployee
	{
		public ShopManager() : base()
		{
		}
    }
}
