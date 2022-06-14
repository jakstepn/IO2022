using ShopModule.Orders;
using ShopModule_ApiClasses.Messages;
using System;
using System.Collections.Generic;

namespace ShopModule.Products
{
    public interface IProduct
    {
        decimal Price { get; set; }
        string ProductName { get; set; }
        bool Available { get; set; }
        Guid Id { get; set; }
        int Quantity { get; set; }
        ProductType ProductType { get; set; }
        ICollection<OrderItem> OrdersItems { get; set; }
        string ProductTypeFK { get; set; }
        ProductMessage Convert(IVisitor visitor);
    }
}
