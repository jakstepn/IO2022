using ShopModule.Orders;
using ShopModule_ApiClasses.Messages;

namespace ShopModule
{
    public interface IVisitor
    {
        public OrderMessage Visit(Order order);
    }
}
