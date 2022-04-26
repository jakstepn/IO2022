using Microsoft.AspNetCore.Mvc;
using ClientModule.Database_Models;
using System.Linq;

using ClientModule_ApiClasses.OrdersModule;

namespace ClientModule.Services
{
    public interface IOrderService
    {
        public ActionResult GetAllCurrentOrders();
        public ActionResult GetOrdersHistory();
        public ActionResult GetChosenOrder(string orderId);
        public ActionResult RejectOrder(string orderId);
        public ActionResult UpdatePaymentStatus(string orderId);

    }

    public class OrderService: IOrderService
    {
        ApplicationDbContext _context;
        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public ActionResult GetAllCurrentOrders()
        {
            GetAllCurrentOrdersResponse response = new();
            return new JsonResult(response);
        }

        public ActionResult GetOrdersHistory()
        {
            GetOrdersHistoryResponse response = new();
            return new JsonResult(response);
        }

        public ActionResult GetChosenOrder(string orderId)
        {
            GetChosenOrderReponse response = new();
            return new JsonResult(response);
        }

        public ActionResult RejectOrder(string orderId)
        {
            RejectOrderResponse response = new();
            return new JsonResult(response);
        }

        public ActionResult UpdatePaymentStatus(string orderId)
        {
            UpdatePaymentStatusResponse response = new();
            return new JsonResult(response);
        }
    }
}
