using System.ComponentModel.DataAnnotations;

namespace ClientModule.Database_Models
{
    [DisplayColumn("ComplaintState")]
    public class ComplaintState
    {
        //We declare an enum to use in code.
        //It will be a primary key so there will not be multiple of them.
        //Enum will be converted to string
        //See OnModelCreating inside ApplicationDbContext to see how it works exactly.
        public enum DbComplaintStateEnum
        {
            WaitingForShopResponse = 0,
            WaitingForClientResponse = 1,
            Finished = 2,
            Closed = 3
        }
        [Key]
        public DbComplaintStateEnum State { get; set; }
    }
}
