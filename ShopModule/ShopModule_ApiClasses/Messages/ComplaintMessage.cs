using System;

namespace ShopModule_ApiClasses.Messages
{
    public enum CurrentComplaintStateMessage { Rejected, Accepted, Pending }
    public class ComplaintMessage
    {
        public Guid complaintId { get; set; }
        public Guid orderId { get; set; }
        public string status { get; set; }
        public string text { get; set; }

        public ComplaintMessage()
        {
        }

    }
}
