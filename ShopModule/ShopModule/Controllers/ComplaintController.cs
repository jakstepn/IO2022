using Complaints;
using Microsoft.AspNetCore.Mvc;
using ShopModule.Data;
using ShopModule.Messages;
using ShopModule.Models;
using ShopModule.Orders;
using ShopModule.Services;
using System.Collections.Generic;
namespace ShopModule.Controllers
{
    [Route("complaints")]
    [ApiController]
    public class ComplaintController : Controller
    {
        private readonly IComplaintService _complaintService;

        public ComplaintController(IComplaintService complaintService)
        {
            _complaintService = complaintService;
        }

        [HttpPost("create")]
        public IActionResult CreateComplaintEndpoint([FromBody] Complaint complaint)
        {
            var res = _complaintService.AddComplaint(complaint);
            if (res != null)
            {
                return ResponseMessage.Success(res, 201);
            }
            else
            {
                return ResponseMessage.Error("Failed to make a complaint", 404);
            }
        }

        [HttpGet("pending/{shopId}")]
        public IActionResult GetPendingComplaintsEndpoint()
        {
            var pending = _complaintService.PendingComplaints();
            if (pending.Count > 0)
            {
                return ResponseMessage.Success(pending, 200);
            }
            else
            {
                return ResponseMessage.Error("Failed to get pending complaints", 404);
            }
        }
        [HttpPut("{complaintID}/accept")]
        public IActionResult AcceptComplaintEndpoint([FromRoute] string complaintId)
        {
            var accepted = _complaintService.AcceptComplaint(complaintId);
            if (accepted != null)
            {
                return ResponseMessage.Success("Accepted complaint.", 200);
            }
            else
            {
                return ResponseMessage.Error("Complaint not found.", 404);
            }
        }
        [HttpPut("{complaintId}/reject")]
        public IActionResult RejectComplaintEndpoint([FromRoute] string complaintId)
        {
            var rejected = _complaintService.RejectComplaint(complaintId);
            if (rejected != null)
            {
                return ResponseMessage.Success("Rejected complaint!", 200);
            }
            else
            {
                return ResponseMessage.Error("Complaint not found.", 404);
            }
        }   
    }
}