using Microsoft.AspNetCore.Mvc;
using System.Linq;

using ClientModule.Data;
using ClientModule.Database_Models; 
using ClientModule_ApiClasses.OrdersModule;
using System.Collections.Generic;

namespace ClientModule.Services
{
    public interface IOrderService
    {
        public GetAllCurrentOrdersResponse GetAllCurrentOrders();
        public GetOrdersHistoryResponse GetOrdersHistory();
        public GetChosenOrderReponse GetChosenOrder(string orderId);
        public RejectOrderResponse RejectOrder(string orderId);
        public UpdatePaymentStatusResponse UpdatePaymentStatus(string orderId);

    }

    public class OrderService: IOrderService
    {
        IApplicationDbContext _context;
        public OrderService(IApplicationDbContext context)
        {
            _context = context;
        }

        public GetAllCurrentOrdersResponse GetAllCurrentOrders()
        {
            GetAllCurrentOrdersResponse response = new();

            Order orders = _context.Orders.Where(c => c.Status.OrderStatus != OrderStatusClass.OrderStatusEnum.Delivered).FirstOrDefault();
            response.orderId = orders.Id;
            response.creationDate = orders.Date;
            response.deliveryDate = orders.EstimateDeliveryDate();
            response.clientAddress = new ResponseAddress { city = orders.DeliveryAddress.City, street = orders.DeliveryAddress.Street, zipCode = orders.DeliveryAddress.Street };
            response.additionalInfo = "";
            response.orderStatus = (ResponseOrderStatus)((int)orders.Status.OrderStatus);
            response.confirmedPayment = false;

            foreach(var product in orders.Products)
            {
                var responseProduct = new ResponseProduct
                {
                    currency = product.Currency,
                    grossPrice = product.Price,
                    orderItemId = product.Id.ToString(),
                    productName = product.Name,
                    quantity = product.Quantity
                };

                response.orderItems.Add(responseProduct);
            }
            return response;
        }

        public GetOrdersHistoryResponse GetOrdersHistory()
        {
            GetOrdersHistoryResponse response = new();
            return response;
        }

        public GetChosenOrderReponse GetChosenOrder(string orderId)
        {
            GetChosenOrderReponse response = new();

            Order order = _context.Orders.Where(c => c.Status.OrderStatus != OrderStatusClass.OrderStatusEnum.Delivered).FirstOrDefault();
            response.orderId = order.Id;
            response.creationDate = order.Date;
            response.deliveryDate = order.EstimateDeliveryDate();
            response.clientAddress = new ResponseAddress { city = order.DeliveryAddress.City, street = order.DeliveryAddress.Street, zipCode = order.DeliveryAddress.Street };
            response.additionalInfo = "";
            response.orderStatus = (ResponseOrderStatus)((int)order.Status.OrderStatus);
            response.confirmedPayment = false;

            foreach (var product in order.Products)
            {
                var responseProduct = new ResponseProduct
                {
                    currency = product.Currency,
                    grossPrice = product.Price,
                    orderItemId = product.Id.ToString(),
                    productName = product.Name,
                    quantity = product.Quantity
                };

                response.orderItems.Append(responseProduct);
            }
            return response;
        }

        public RejectOrderResponse RejectOrder(string orderId)
        {
            RejectOrderResponse response = new();
            var order = _context.Orders.Where(c => c.Id == orderId).FirstOrDefault();
            if(order == null)
            {
                throw new KeyNotFoundException();
            }

            if (canOrderBeRejected(order))
            {
                response.orderStatus = ResponseOrderStatus.RejectedByCustomer;
            }
            else
            {
                response.orderStatus = (ResponseOrderStatus)(int)order.Status.OrderStatus;
            }
            return response;
        }
        bool canOrderBeRejected(Order order)
        {
            if(order.Status.OrderStatus == OrderStatusClass.OrderStatusEnum.RejectedByCustomer ||
                order.Status.OrderStatus == OrderStatusClass.OrderStatusEnum.RejectedByShop ||
                order.Status.OrderStatus == OrderStatusClass.OrderStatusEnum.Delivered)
            {
                return false;
            }

            return true;
        }
        public UpdatePaymentStatusResponse UpdatePaymentStatus(string orderId)
        {
            UpdatePaymentStatusResponse response = new();
            return response;
        }
    }
}
