using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ClientModule.Database_Models
{
    [DisplayColumn("Complaint")]
    [Index(nameof(DbComplaint.Complaint))]
    public class DbComplaint
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public virtual DbComplaint Complaint { get; set; }
    }
}