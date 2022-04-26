using Microsoft.AspNetCore.Mvc;
using ClientModule.Database_Models;

namespace ClientModule.Services
{
    public interface IOrderService
    {
        public ActionResult GetAllCurrentOrders();
        public ActionResult GetOrdersHistory();
        public ActionResult RejectOrder();
        public ActionResult UpdatePaymentStatus();

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
            throw new System.NotImplementedException();
        }

        public ActionResult GetOrdersHistory()
        {
            throw new System.NotImplementedException();
        }

        public ActionResult RejectOrder()
        {
            throw new System.NotImplementedException();
        }

        public ActionResult UpdatePaymentStatus()
        {
            throw new System.NotImplementedException();
        }
    }
}
