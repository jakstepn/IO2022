﻿using ShopModule.Data;
using ShopModule.Models;
using ShopModule.Orders;
using ShopModule.Products;
using ShopModule_ApiClasses.Messages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopModule.Services
{
    public interface IOrderService
    {
        OrderMessage ChangeStatus(Guid orderId, OrderStatus staus);
        OrderMessage FindOrder(Guid orderId);
        OrderItem[] AddOrderItems(OrderItem[] items);
        OrderItemMessage[] AddOrderAndItems(OrderItemMessage[] items, Order order);
        OrderItem AddOrderItem(OrderItem item);
        OrderMessage AddOrder(Order order);
        List<OrderMessage> FindPendingOrders();
        OrderMessage RemoveOrder(Guid orderId);
        void NotifyDeliveryStatusOfStatus(OrderStatus status);
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
                if (item.Product.Available)
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
                if(count == 0)
                {
                    item.Product.Available = false;
                }
            }
            bool saved = _context.SaveChanges() == items.Length * 2;
            return saved ? items : null;
        }

        /// <summary>
        /// Add every order Item from given order to the database based on a json message
        /// </summary>
        /// <param name="items"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public OrderItemMessage[] AddOrderAndItems(OrderItemMessage[] items, Order order)
        {
            List<Product> products = new List<Product>();
            foreach (var item in items)
            {
                var product = _context.Products.Find(item.productName);
                if (product != null && product.Available)
                {
                    _context.OrderItems.Add(new OrderItem(item, order, product));
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

            _context.Orders.Add(order);
            _context.SaveChanges();

            return items;
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
        public OrderMessage AddOrder(Order order)
        {
            _context.Orders.Add(order);
            bool saved = _context.SaveChanges() == 1;
            return saved ? order.Convert(StaticData.defaultConverter) : null;
        }

        /// <summary>
        /// Get all of the orders that have their status set as Pending
        /// </summary>
        /// <returns>Returns an array of Orders</returns>
        public List<OrderMessage> FindPendingOrders()
        {
            List<OrderMessage> orders = new List<OrderMessage>();
            foreach (var ord in _context.Orders.Where(x => x.OrderStatus == OrderStatus.Pending).ToList())
            {
                LoadOrder(ord);

                orders.Add(ord.Convert(StaticData.defaultConverter));
            }
            return orders;
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
            o.ChangeStatus(status);
            if (_context.SaveChanges() == 1)
            {
                return o.Convert(StaticData.defaultConverter);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Notify Delivery Module of an order status change
        /// </summary>
        /// <param name="status">New order status</param>
        public void NotifyDeliveryStatusOfStatus(OrderStatus status)
        {
            //TODO
            // Add module message

            switch (status)
            {
                case OrderStatus.WaitingForCollection:
                    // Notify: Ready to pickup
                    break;
                case OrderStatus.Collecting:
                    break;
                case OrderStatus.WaitingForCourier:
                    break;
                case OrderStatus.OnTheWay:
                    break;
                case OrderStatus.RejectedByShop:
                    // Nofity: Reject order
                    break;
                case OrderStatus.RejectedByCustomer:
                    break;
                case OrderStatus.Pending:
                    // Notify: In preparation
                    break;
                default:
                    break;
            }
        }

        private void NotifyClientPackageCollected()
        {

        }

        private void NotifyClientPackageDelivered()
        {

        }

        private void LoadOrder(Order o)
        {
            _context.Entry(o).Collection(p => p.Items).Load();
            _context.Entry(o).Reference(p => p.ClientAddress).Load();
        }
    }
}
