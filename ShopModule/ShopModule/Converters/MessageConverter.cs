using ShopModule.Orders;
using ShopModule_ApiClasses.Messages;

namespace ShopModule.Converters
{
    public class MessageConverter : IVisitor
    {
        public OrderMessage Visit(Order order)
        {
            OrderItemMessage[] items = null;
            if (order.Items != null)
            {
                items = new OrderItemMessage[order.Items.Count];
                int i = 0;
                foreach (var item in order.Items)
                {
                    items[i] = new OrderItemMessage
                    {
                        currency = item.Currency,
                        grossPrice = item.GrossPrice,
                        orderItemId = item.Id,
                        productName = item.ProductName,
                        quantity = item.Quantity,
                    };
                    i++;
                }
            }
            return new OrderMessage
            {
                additionalInfo = order.AdditionalInfo,
                clientAddress = new AddressMessage { city = order.ClientAddress.City, street = order.ClientAddress.Street, zipCode = order.ClientAddress.ZipCode },
                confirmedPayment = order.ConfirmedPayment,
                creationDate = order.CreationDate,
                deliveryDate = order.DeliveryDate,
                orderItems = items,
                orderId = order.Id,
                orderStatus = (OrderStatusMessage)order.OrderStatus,
            };
        }
    }
}
