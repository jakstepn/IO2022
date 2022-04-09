using Complaints;
using Microsoft.AspNetCore.Mvc;
using ShopModule.Data;
using System.Collections.Generic;
namespace ShopModule.Controllers
{
    [ApiController]
    [Route("complaints")]
    public class ComplaintController : Controller
    {
        private readonly ShopModuleDbContext _context;

        public ComplaintController(ShopModuleDbContext context)
        {
            _context = context;
        }

        [HttpGet("/complaints/create")]
        public IActionResult CreateComplaint([FromBody] Complaint complaint)
        {
            // TODO
            // Add a complaint to the database

            bool addedComplaint = false;
            if (addedComplaint)
            {
                var res = new JsonResult(complaint);
                res.StatusCode = 201;
                return res;
            }
            else
            {
                var res = new JsonResult("Failed to make a complaint");
                res.StatusCode = 404;
                return res;
            }
        }
        [HttpGet("/complaints/pending/{shopId}")]
        public IActionResult GetPendingComplaints([FromRoute] int shopId)
        {
            var shop = _context.Shop.Find(shopId);
            if (shop != null)
            {
                // TODO
                // create an array of complaints

                var arr = new List<string>();
                var res = new JsonResult(arr);
                res.StatusCode = 200;
                return res;
            }
            else
            {
                var res = new JsonResult("Failed to get pending complaints");
                res.StatusCode = 404;
                return res;
            }
        }
        [HttpPut("/complaints/{complaintID}/accept")]
        public IActionResult AccepComplaint([FromRoute] string complaintId)
        {
            // TODO
            // Accept Complaint

            var res = new JsonResult("");
            if (complaintId != string.Empty)
            {
                res.Value = "Accepted complaint!";
                res.StatusCode = 200;
            }
            else
            {
                res.Value = "Complaint not found!";
                res.StatusCode = 404;
            }
            return res;
        }
        [HttpPut("/complaints/{complaintId}/reject")]
        public IActionResult RejectComplaint([FromRoute] string complaintId)
        {
            // TODO
            // Reject Complaint

            var res = new JsonResult("");
            if (complaintId != string.Empty)
            {
                res.Value = "Rejected complaint!";
                res.StatusCode = 200;
            }
            else
            {
                res.Value = "Complaint not found!";
                res.StatusCode = 404;
            }
            return res;
        }
    }
}