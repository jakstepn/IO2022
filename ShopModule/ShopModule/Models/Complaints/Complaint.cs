using ShopModule.Complaints;
using ShopModule_ApiClasses.Messages;
using System;
using System.ComponentModel.DataAnnotations;

namespace Complaints
{
    public class Complaint
    {
        [Key]
        public string Id { get; set; }
        public string Text { get; set; }
        public CurrentComplaintState CurrentStatus { get; set; }

        public Complaint()
        {

        }

        public Complaint(ComplaintMessage message)
        {
            Id = message.complaintId;
            Text = message.text;
            CurrentStatus = (CurrentComplaintState)message.status;
        }
    }
}
