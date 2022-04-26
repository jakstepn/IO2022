using Complaints;
using ShopModule.Complaints;

namespace ShopModule.Messages
{
    public class ComplaintMessage
    {
        public string complaintId { get; set; }
        public string orderId { get; set; }
        public CurrentState status { get; set; }
        public string text { get; set; }

        public ComplaintMessage()
        {
        }

        public ComplaintMessage(Complaint complaint)
        {
            complaintId = complaint.Id;
            orderId = "";
            status = complaint.CurrentStatus;
            text = complaint.Text;
        }
    }
}
