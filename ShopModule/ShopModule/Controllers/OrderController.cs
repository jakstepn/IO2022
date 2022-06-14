using Microsoft.AspNetCore.Mvc;
using ShopModule.Data;
using ShopModule.Employees;
using ShopModule.Models;
using ShopModule.Orders;
using ShopModule.Services;
using ShopModule_ApiClasses.Messages;
using ShopModule_ApiClasses.Messages.Request;
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

        /// <summary>
        /// Create a new order
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpPost()]
        public IActionResult PlaceOrder([FromBody] RequestOrderMessage message)
        {
            bool success = false;
            try
            {
                var order = _orderService.AddOrderAndItems(message);
                if (order != null)
                {
                    success = true;
                }

                if (success)
                {
                    return ResponseMessage.Success(order, 201);
                }
                return ResponseMessage.Error("Failed to create order", 404);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get all shop orders paginated
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        [HttpGet("shop")]
        public IActionResult GetOrdersAssignedToShop([FromQuery] int page, [FromQuery] int pageSize)
        {
            var pending = _orderService.FindOrdersPaginated(page, pageSize);
            if (pending.Count > 0)
            {
                return ResponseMessage.Success(pending, 200);
            }
            else
            {
                return ResponseMessage.Error("Failed to get pending orders", 404);
            }
        }

        /// <summary>
        /// Get all pending shop orders paginated
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        [HttpGet("shop/pending")]
        public IActionResult GetPendingOrdersAssignedToShop([FromQuery] int page, [FromQuery] int pageSize)
        {
            var pending = _orderService.FindPendingOrdersPaginated(page, pageSize);
            if (pending.Count > 0)
            {
                return ResponseMessage.Success(pending, 200);
            }
            else
            {
                return ResponseMessage.Error("Failed to get pending orders", 404);
            }
        }

        /// <summary>
        /// Get chosen order
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
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
        [HttpPatch("{orderId}")]
        public IActionResult SetChosenOrder([FromRoute] Guid orderId, [FromBody] string string_status)
        {
            OrderStatus status;
            bool accepted_enum = Enum.TryParse(string_status, out status);
            OrderMessage order;
            bool notified;
            if (accepted_enum && (order = _orderService.ChangeStatus(orderId, status)) != null)
            {
                if (status == OrderStatus.PickedUpByCourier)
                {
                    // TODO
                    // Notify Client package collected
                    notified = true;
                }
                else if (status == OrderStatus.Delivered)
                {
                    // TODO
                    // Notify client package delivered
                    _orderService.UpdateDeliveryTime(orderId, DateTime.Now);
                    notified = true;
                }
                else
                {
                    notified = _orderService.NotifyDeliveryStatusOfStatus(status, orderId);
                }
                if (notified)
                {
                    return ResponseMessage.Success(order, 204);
                }
            }
            return ResponseMessage.Error("Order doesn't exist!", 404);
        }
    }
}