using ShopModule.Complaints;
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
    }
}
