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

        /// <summary>
        /// Get a complaint
        /// </summary>
        /// <param name="complaintId"></param>
        /// <returns></returns>
        [HttpGet("{complaintId}")]
        public IActionResult GetChosenComplaint([FromRoute] Guid complaintId)
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

        /// <summary>
        /// Create a complaint
        /// </summary>
        /// <param name="complaint"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get all pending complaints
        /// </summary>
        /// <returns></returns>
        [HttpGet("pending/{shopId}")]
        public IActionResult GetPendingComplaintsEndpoint([FromQuery] int page, [FromQuery] int pageSize)
        {
            var pending = _complaintService.PendingComplaintsPaginated(page, pageSize);
            if (pending.Count > 0)
            {
                return ResponseMessage.Success(pending, 200);
            }
            else
            {
                return ResponseMessage.Error("Failed to get pending complaints", 404);
            }
        }

        /// <summary>
        /// Accept complaint
        /// </summary>
        /// <param name="complaintId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Reject complaint
        /// </summary>
        /// <param name="complaintId"></param>
        /// <returns></returns>
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