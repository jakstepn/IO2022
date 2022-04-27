namespace ShopModule_ApiClasses.Messages
{
    public enum CurrentComplaintStateMessage { Rejected, Accepted, Pending }
    public class ComplaintMessage
    {
        public string complaintId { get; set; }
        public string orderId { get; set; }
        public CurrentComplaintStateMessage status { get; set; }
        public string text { get; set; }

        public ComplaintMessage()
        {
        }

    }
}
