using Complaints;
using ShopModule.Orders;
using ShopModule.Products;
using ShopModule_ApiClasses.Messages;

namespace ShopModule
{
    public interface IVisitor
    {
        public OrderMessage Visit(Order order);
        public ProductMessage Visit(Product order);
        public ComplaintMessage Visit(Complaint order);
    }
}
