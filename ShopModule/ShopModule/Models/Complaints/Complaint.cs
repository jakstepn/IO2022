using ShopModule;
using ShopModule.Complaints;
using ShopModule_ApiClasses.Messages;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Complaints
{
    [Table("Complaints")]
    public class Complaint
    {
        [Key]
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public string Text { get; set; }
        public string CurrentStatus { get; set; }

        public Complaint()
        {

        }

        public Complaint(ComplaintMessage message)
        {
            Id = message.complaintId;
            Text = message.text;
            OrderId = message.orderId;
            CurrentStatus = message.status;
        }

        public ComplaintMessage Convert(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
