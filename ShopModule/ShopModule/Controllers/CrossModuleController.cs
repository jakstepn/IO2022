using Microsoft.AspNetCore.Mvc;

namespace ShopModule.Controllers
{
    public class CrossModuleController : Controller
    {
        [HttpPut]
        public IActionResult NotifyClientComplaintRejected()
        {
            return null;
        }
        [HttpPut]
        public IActionResult NotifyClientComplaintAccepted()
        {
            return null;
        }
        [HttpPost]
        public IActionResult NotifyCourierPackageReady()
        {
            return null;
        }
        [HttpPost]
        public IActionResult NotifyClientShopRejectedOrder()
        {
            return null;
        }
        [HttpPut]
        public IActionResult NotifyClientPackagePickedUp()
        {
            return null;
        }
        [HttpPut]
        public IActionResult NotifyClientPackageDelivered()
        {
            return null;
        }
    }
}
