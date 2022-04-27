using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ClientModule.Database_Models
{
    [DisplayColumn("Complaint")]
    [Index(nameof(Database_Models.Complaint.ComplaintFK))]
    public class Complaint
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public virtual Complaint ComplaintFK { get; set; }
    }
}