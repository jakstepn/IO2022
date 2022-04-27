using Microsoft.AspNetCore.Mvc;
using ShopModule.Data;
using ShopModule.Employees;
using ShopModule.Models;
using ShopModule.Orders;
using ShopModule.Services;
using ShopModule_ApiClasses.Messages;
using System.Collections.Generic;

namespace ShopModule.Controllers
{
    [Route("orders")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("place")]
        public IActionResult PlaceOrder([FromBody] OrderMessage message)
        {
            int len = message.orderItems.Length;
            OrderItem[] items = new OrderItem[len];
            bool allItemsExist = true;
            for (int i = 0; i < len; i++)
            {
                items[i] = new OrderItem(message.orderItems[i]);
                if (!(allItemsExist = items[i].Product.Available) &&
                    !(allItemsExist = items[i].Product.Quantity-items[i].Quantity < 0))
                {
                    break;
                }
            }
            Order changeStatus()
            {
                var order = new Order(message);
                order.OrderStatus = OrderStatus.Pending;
                return order;
            }
            if (allItemsExist && _orderService.AddOrder(changeStatus()) != null &&
                _orderService.AddOrderItems(items) != null)
            {
                foreach (var item in items)
                {
                    var product = _orderService.GetProduct(item.ProductFK);
                    product.Quantity -= item.Quantity;
                    if (product != null && product.Quantity <= 0)
                    {
                        product.Available = false;
                    }
                }
                return ResponseMessage.Success(message, 201);
            }
            else
            {
                _orderService.RemoveOrder(message.orderId);
                return ResponseMessage.Error("Failed to create order", 404);
            }
        }

        [HttpGet("pending/{shopId}")]
        public IActionResult GetPendingOrdersAssignedToShop([FromRoute] string shopId)
        {
            var pending = _orderService.FindPendingOrders();
            if (pending.Count > 0)
            {
                return ResponseMessage.Success(pending, 200);
            }
            else
            {
                return ResponseMessage.Error("Failed to get pending orders", 404);
            }
        }
        [HttpGet("{orderId}")]
        public IActionResult GetChosenOrder([FromRoute] string orderId)
        {
            Order order = _orderService.FindOrder(orderId);
            if (order != null)
            {
                return ResponseMessage.Success(order, 200);
            }
            else
            {
                return ResponseMessage.Error("Failed to get this order.", 404);
            }
        }
        /// <summary>
        /// Set status for a chosen order
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpPut("{orderId}")]
        public IActionResult SetChosenOrder([FromRoute] string orderId, [FromBody] OrderStatus status)
        {
            Order order = _orderService.FindOrder(orderId);
            if (order != null)
            {
                order.ChangeStatus(status);
                if (status == OrderStatus.WaitingForCollection)
                {
                    ShopEmployee shopEmployee = new ShopEmployee();
                    shopEmployee.NotifyDeliveryThatPackageIsReady(order);
                    order.ChangeStatus(OrderStatus.WaitingForCourier);
                }
                if (status == OrderStatus.ParcelCollected)
                {
                    NotifyClientPackageCollected();
                }
                if (status == OrderStatus.Delivered)
                {
                    NotifyClientPackageDelivered();
                }
                return ResponseMessage.Success(order, 200);
            }
            else
            {
                return ResponseMessage.Error("Order doesn't exist!", 404);
            }
        }

        private void NotifyClientPackageCollected()
        {

        }

        private void NotifyClientPackageDelivered()
        {

        }
    }
}