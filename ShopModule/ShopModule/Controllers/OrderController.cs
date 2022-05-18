using Microsoft.AspNetCore.Mvc;
using ShopModule.Data;
using ShopModule.Employees;
using ShopModule.Models;
using ShopModule.Orders;
using ShopModule.Services;
using ShopModule_ApiClasses.Messages;
using System;
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
            bool success = false;
            try
            {
                var messages = _orderService.AddOrderAndItems(message.orderItems, message);
                if(messages != null)
                {
                    success = true;
                }

                if (success)
                {
                    bool notified = _orderService.NotifyDeliveryStatusOfStatus((OrderStatus)Enum.Parse(typeof(OrderStatus), message.orderStatus), message.orderId);
                    if (notified)
                    {
                        return ResponseMessage.Success(message, 201);
                    }
                }
                    return ResponseMessage.Error("Failed to create order", 404);
            }
            catch (Exception)
            {
                throw;
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
        public IActionResult GetChosenOrder([FromRoute] Guid orderId)
        {
            OrderMessage order = _orderService.FindOrder(orderId);
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
        public IActionResult SetChosenOrder([FromRoute] Guid orderId, [FromBody] string string_status)
        {
            OrderStatus status;
            bool accepted_enum = Enum.TryParse<OrderStatus>(string_status, out status);
            OrderMessage order;
            bool notified;
            if (accepted_enum && (order = _orderService.ChangeStatus(orderId, status)) != null)
            {
                if (status == OrderStatus.ParcelCollected)
                {
                    // TODO
                    // Notify Client package collected
                    notified = true;
                }
                else if (status == OrderStatus.Delivered)
                {
                    // TODO
                    // Notify client package delivered
                    notified = true;
                }
                else
                {
                    notified = _orderService.NotifyDeliveryStatusOfStatus(status, orderId);
                }
                if (notified)
                {
                    return ResponseMessage.Success(order, 200);
                }
            }
            return ResponseMessage.Error("Order doesn't exist!", 404);
        }
    }
}