using ShopModule.Data;
using ShopModule.Orders;
using System.Collections.Generic;
using System.Linq;

namespace ShopModule.Services
{
    public interface IOrderService
    {
        Order FindOrder(string orderId);
        OrderItem[] AddOrderItems(OrderItem[] items);
        OrderItem AddOrderItem(OrderItem item);
        Order AddOrder(Order order);
        List<Order> FindPendingOrders();
        public Order RemoveOrder(string orderId);
    }
    public class OrderService : IOrderService
    {
        private readonly ShopModuleDbContext _context;
        public OrderService(ShopModuleDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Find an order in a database
        /// </summary>
        /// <param name="productId">Id of the element to be found</param>
        /// <returns>Returns the first matching order on success and a null on failure</returns>
        public Order FindOrder(string orderId)
        {
            return _context.Orders.Find(orderId);
        }

        /// <summary>
        /// Adds every order item from the array to the database
        /// </summary>
        /// <param name="items">Order items to be added</param>
        /// <returns>Return an array if success, else null</returns>
        public OrderItem[] AddOrderItems(OrderItem[] items)
        {
            foreach (var item in items)
            {
                _context.OrderItems.Add(item);
            }
            bool saved = _context.SaveChanges() == items.Length;
            return saved ? items : null;
        }
        
        /// <summary>
        /// Add order item to the database
        /// </summary>
        /// <param name="item">OrderItem to be added</param>
        /// <returns>Returns an order item if success, else null</returns>
        public OrderItem AddOrderItem(OrderItem item)
        {
            _context.OrderItems.Add(item);
            bool saved = _context.SaveChanges() == 1;
            return saved ? item : null;
        }

        /// <summary>
        /// Add order to the database
        /// </summary>
        /// <param name="order">Order to be added</param>
        /// <returns>Returns an order if success, else null</returns>
        public Order AddOrder(Order order)
        {
            _context.Orders.Add(order);
            bool saved = _context.SaveChanges() == 1;
            return saved ? order : null;
        }

        /// <summary>
        /// Get all of the orders that have their status set as Pending
        /// </summary>
        /// <returns>Returns an array of Orders</returns>
        public List<Order> FindPendingOrders()
        { 
            return _context.Orders.Where(x => x.OrderStatus == OrderStatus.Pending).ToList();
        }

        /// <summary>
        /// Remove an order from the database
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns>Reurn removed order on succes, null if such doesn't exist</returns>
        public Order RemoveOrder(string orderId)
        {
            var res = _context.Orders.Find(orderId);
            if(res != null)
            {
                _context.Orders.Remove(res);
            }
            return res;
        }
    }
}
