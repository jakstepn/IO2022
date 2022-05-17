﻿using ShopModule_ApiClasses.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopModule.Products
{
	[Table("ProductTypes")]
	public class ProductType
	{
        [Key]
		public string Name { get; set; }
		public ICollection<Product> Products { get; set; }

		public ProductType()
        {
        }

		public ProductType(ProductTypeMessage message)
		{
			Name = message.name;
		}
	}
}
