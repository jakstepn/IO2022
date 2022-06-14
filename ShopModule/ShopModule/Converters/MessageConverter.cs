using Complaints;
using ShopModule.Orders;
using ShopModule.Products;
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
                        grossPrice = item.Product.Price,
                        productName = item.Product.ProductName,
                        quantity = item.Quantity,
                    };
                    i++;
                }
            }
            return new OrderMessage
            {
                additionalInfo = order.AdditionalInfo,
                clientAddress = order.ClientAddress != null ? new AddressMessage
                {
                    city = order.ClientAddress.City,
                    street = order.ClientAddress.Street,
                    zipCode = order.ClientAddress.ZipCode
                } : null,
                creationDate = order.CreationDate,
                deliveryDate = order.DeliveryDate,
                orderItems = items,
                orderId = order.Id,
                orderStatus = order.OrderStatus,
            };
        }

        public ComplaintMessage Visit(Complaint complaint)
        {
            return new ComplaintMessage
            {
                complaintId = complaint.Id,
                orderId = complaint.OrderId,
                status = complaint.CurrentStatus,
                text = complaint.Text
            };
        }

        public ProductMessage Visit(Product product)
        {
            return new ProductMessage
            {
                productId = product.Id,
                category = product.ProductTypeFK,
                name = product.ProductName,
                price = product.Price,
                quantity = product.Quantity
            };
        }
    }
}
