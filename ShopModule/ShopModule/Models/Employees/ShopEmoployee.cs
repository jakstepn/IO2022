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
using ShopModule.Models.Messages;
using ShopModule.Orders;
using ShopModule.Products;

namespace ShopModule.Employees
{
    public class ShopEmployee
    {
        [Key]
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime EmployedSince { get; set; }
        public CurrentState CurrentState { get; set; }
        public Shop Shop { get; set; }

        public ShopEmployee()
        {
        }

        public void ChangeOrderStatus(Order order, OrderStatus status) { }
        public OrderStatus GetOrderStatus(Order order) { return OrderStatus.Collecting; }
        public void RejectOrder(Order order) { order.OrderStatus = OrderStatus.RejectedByShop; }
        public void SetProductAsUnavailable(Product product) => product.Available = false;
        public async void NotifyDeliveryThatPackageIsReady(Order order)
        {
            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(StaticData.urlRequestPickup,
                    new StringContent(
                        JsonSerializer.Serialize(order.Id)));
            }
        }

        // Database relations
        [ForeignKey("Shop")]
        public string ShopFK { get; set; }
    }
}
