using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ClientModule.Database_Models
{
    [DisplayColumn("Complaint")]
    public class Complaint
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public ComplaintState complaintState { get; set; }
    }
}