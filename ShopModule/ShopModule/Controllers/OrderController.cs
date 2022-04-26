using Microsoft.AspNetCore.Mvc;
using Models.Messages;
using ShopModule.Data;
using ShopModule.Models;
using ShopModule.Orders;
using ShopModule.Services;
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
            if (_orderService.AddOrderItems(message.orderItems) != null)
            {
                return ResponseMessage.Success(message, 201);
            }
            else
            {
                return ResponseMessage.Error("Failed to create order", 404);
            }
        }

        [HttpGet("pending/{shopId}")]
        public IActionResult GetPendingOrdersAssignedToShop([FromRoute] string shopID)
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
        [HttpPost("{orderId}")]
        public IActionResult SetChosenOrder([FromRoute] string orderId, [FromBody] OrderStatus status)
        {
            Order order = _orderService.FindOrder(orderId);
            if (order != null)
            {
                order.ChangeStatus(status);
                return ResponseMessage.Success(order, 200);
            }
            else
            {
                return ResponseMessage.Error("Order doesn't exist!", 404);
            }
        }
    }
}