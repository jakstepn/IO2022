using Complaints;
using Microsoft.AspNetCore.Mvc;
using ShopModule.Data;
using ShopModule.Models;
using ShopModule.Orders;
using ShopModule.Services;
using ShopModule_ApiClasses.Messages;
using System;
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

        [HttpGet("{complaintId}")]
        public IActionResult GetChosenComplaint([FromBody] Guid complaintId)
        {
            var res = _complaintService.GetComplaint(complaintId);
            if (res != null)
            {
                return ResponseMessage.Success(res.Convert(StaticData.defaultConverter), 200);
            }
            else
            {
                return ResponseMessage.Error("Failed to make a complaint", 404);
            }
        }

        [HttpPost("create")]
        public IActionResult CreateComplaintEndpoint([FromBody] ComplaintMessage complaint)
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
        [HttpPut("{complaintId}/accept")]
        public IActionResult AcceptComplaintEndpoint([FromRoute] Guid complaintId)
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
        public IActionResult RejectComplaintEndpoint([FromRoute] Guid complaintId)
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