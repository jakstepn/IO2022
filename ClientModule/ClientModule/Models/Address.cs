using System.ComponentModel.DataAnnotations;

namespace ClientModule.Database_Models
{
    //It was my personal freedom to make it like this.
    [DisplayColumn("Address")]
    public class Address
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string PostalCode { get; set; }
        [Required]
        //There are quite a lot of addresses with complex street numbers ( 5A, 6/8 etc)
        public string StreetNumber { get; set; }
        
        public int ApartmentNumber { get; set; }
    }
}