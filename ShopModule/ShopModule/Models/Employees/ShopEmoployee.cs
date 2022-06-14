using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using ShopModule.Models;
using ShopModule.Orders;
using ShopModule.Products;

namespace ShopModule.Employees
{
    [Table("ShopEmployees")]
    public class ShopEmployee
    {
        [Key]
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime EmployedSince { get; set; }
        public CurrentState CurrentState { get; set; }

        public ShopEmployee()
        {
        }

        public void ChangeOrderStatus(Order order, OrderStatus status) => order.OrderStatus = status.ToString(); 
        public OrderStatus GetOrderStatus(Order order) => (OrderStatus)Enum.Parse(typeof(OrderStatus), order.OrderStatus);
        public void RejectOrder(Order order) => order.OrderStatus = OrderStatus.RejectedByShop.ToString();
        public void SetProductAsUnavailable(Product product) => product.Available = false;
    }
}
