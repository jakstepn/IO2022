using ShopModule.Orders;
using ShopModule_ApiClasses.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopModule.Products
{
	[Table("Products")]
	public class Product : IProduct
	{
		[Column(TypeName = "decimal(18,4)")]
		public virtual decimal Price { get; set; }
		public virtual int TaxRate { get; set; }
        [Key]
		public virtual string ProductName { get; set; }
		public virtual bool Available { get; set; }
		public virtual int Quantity { get; set; }
		public virtual ProductType ProductType { get; set; }
		public virtual ICollection<OrderItem> OrdersItems { get; set; }
		public Product()
		{
		}

		public Product(ProductMessage message, ProductType category)
        {
			Price = message.price;
			Quantity = message.quantity;
			ProductType = category;
			ProductName = message.name;
			Available = Quantity > 0;
        }

		public virtual ProductMessage Convert(IVisitor visitor)
        {
			return visitor.Visit(this);
        }

		[ForeignKey("ProductType")]
		public virtual string ProductTypeFK { get; set; }
	}
}
