using ShopModule.Converters;
using ShopModule.Data;
using ShopModule.Location;
using ShopModule.Models;
using ShopModule.Orders;
using ShopModule.Products;
using ShopModule_ApiClasses.Messages;
using ShopModule_ApiClasses.Messages.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.RegularExpressions;

namespace ShopModule.Services
{
    public interface IOrderService
    {
        OrderMessage ChangeStatus(Guid orderId, OrderStatus staus);
        OrderMessage FindOrder(Guid orderId);
        OrderItem[] AddOrderItems(OrderItem[] items);
        OrderMessage AddOrderAndItems(RequestOrderMessage order);
        List<OrderMessage> FindPendingOrdersPaginated(int page, int pageSize);
        List<OrderMessage> FindOrdersPaginated(int page, int pageSize);
        OrderMessage RemoveOrder(Guid orderId);
        bool UpdateDeliveryTime(Guid orderId, DateTime deliveryDate);
        bool NotifyDeliveryStatusOfStatus(OrderStatus status, Guid guid);
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
        public OrderMessage FindOrder(Guid orderId)
        {
            Order o = _context.Orders.Find(orderId);
            LoadOrder(o);
            return o != null ? o.Convert(StaticData.defaultConverter) : null;
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
                if (Regex.IsMatch(item.Currency, StaticData.regexCurrency) && item.Product.Available)
                {
                    _context.OrderItems.Add(item);
                }
                else
                {
                    return null;
                }
            }
            foreach (var item in items)
            {
                item.Product.Quantity -= item.Quantity;
                int count = item.Product.Quantity;
                if (count < 0)
                {
                    return null;
                }
                if (count == 0)
                {
                    item.Product.Available = false;
                }
            }
            _context.SaveChanges();
            return items;
        }

        /// <summary>
        /// Add every order Item from given order to the database based on a json message
        /// </summary>
        /// <param name="items"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public OrderMessage AddOrderAndItems(RequestOrderMessage message)
        {
            List<Product> products = new List<Product>();
            Address a = new Address(message.clientAddress);
            Order order = new Order(message, a);
            RequestOrderItemMessage[] items = message.orderItems;
            foreach (var item in items)
            {
                var product = _context.Products.Find(item.productId);
                if (product != null && product.Available)
                {
                    _context.OrderItems.Add(new OrderItem(order, product, item.quantity));
                    products.Add(product);
                }
                else
                {
                    return null;
                }
            }

            int i = 0;
            // Reduce product quantity
            foreach (var prod in products)
            {
                prod.Quantity -= items[i].quantity;
                int count = prod.Quantity;
                if (count < 0)
                {
                    return null;
                }
                if (count == 0)
                {
                    prod.Available = false;
                }
                i++;
            }
            _context.Addresses.Add(a);
            _context.Orders.Add(order);
            _context.SaveChanges();
            return order.Convert(new MessageConverter());
        }

        /// <summary>
        /// Get orders that have their status set as Pending in paginated fashion
        /// </summary>
        /// <returns>Returns an array of Orders</returns>
        public List<OrderMessage> FindPendingOrdersPaginated(int page, int pageSize)
        {
            List<OrderMessage> orders = new List<OrderMessage>();
            foreach (var ord in _context.Orders.Where(x => x.OrderStatus == OrderStatus.Pending.ToString())
                .ToList().OrderByDescending(x => x.CreationDate))
            {
                LoadOrder(ord);

                orders.Add(ord.Convert(StaticData.defaultConverter));
            }
            return orders.Skip(page * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// Get orders
        /// </summary>
        /// <returns>Returns an array of Orders</returns>
        public List<OrderMessage> FindOrdersPaginated(int page, int pageSize)
        {
            List<OrderMessage> orders = new List<OrderMessage>();
            foreach (var ord in _context.Orders.ToList().OrderByDescending(x => x.CreationDate))
            {
                LoadOrder(ord);

                orders.Add(ord.Convert(StaticData.defaultConverter));
            }
            return orders.Skip(page * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// Remove an order from the database
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns>Reurn removed order on succes, null if such doesn't exist</returns>
        public OrderMessage RemoveOrder(Guid orderId)
        {
            var res = _context.Orders.Find(orderId);
            if (res != null)
            {
                _context.Orders.Remove(res);
                _context.SaveChanges();
            }
            return res != null ? res.Convert(StaticData.defaultConverter) : null;
        }

        /// <summary>
        /// Get product from a database
        /// </summary>
        /// <param name="productId"></param>
        /// <returns>Rerurns product if exists, else null</returns>
        public Product GetProduct(string productId)
        {
            return _context.Products.Find(productId);
        }

        /// <summary>
        /// Change order status
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public OrderMessage ChangeStatus(Guid orderId, OrderStatus status)
        {
            Order o = _context.Orders.Find(orderId);
            if (o != null)
            {
                LoadOrder(o);
                o.ChangeStatus(status);
                _context.SaveChanges();
                return o.Convert(StaticData.defaultConverter);
            }
            return null;
        }

        /// <summary>
        /// Update delivery time
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="deliveryDate"></param>
        /// <returns>True if delivery time has been changed</returns>
        public bool UpdateDeliveryTime(Guid orderId, DateTime deliveryDate)
        {
            var order = _context.Orders.Find(orderId);
            if (order != null)
            {
                order.DeliveryDate = deliveryDate;
                return _context.SaveChanges() == 1;
            }
            return false;
        }

        /// <summary>
        /// Notify Delivery Module of an order status change
        /// </summary>
        /// <param name="status">New order status</param>
        /// <returns>True if delivery notfied</returns>
        public bool NotifyDeliveryStatusOfStatus(OrderStatus status, Guid guid)
        {
            //TODO
            // Add module message

            switch (status)
            {
                case OrderStatus.ReadyForDelivery:
                    // Notify: Ready to pickup
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(StaticData.urlDeliveryModule);

                        var getTask = client.PostAsJsonAsync<Guid>($"requestPickup", guid).Result;
                        if (!getTask.IsSuccessStatusCode)
                        {
                            return false;
                        }
                    }
                    return true;
                case OrderStatus.RejectedByShop:
                    // Nofity: Reject order
                case OrderStatus.RejectedByCustomer:
                case OrderStatus.Pending:
                    // Notify: In preparation
                case OrderStatus.InPreparation:
                    return true;
                default:
                    return false;
            }
        }

        private void LoadOrder(Order o)
        {
            _context.Entry(o).Collection(p => p.Items).Load();
            foreach (var item in o.Items)
            {
                _context.Entry(item).Reference(p => p.Product).Load();
            }
            _context.Entry(o).Reference(p => p.ClientAddress).Load();
        }
    }
}
