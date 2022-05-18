using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClientModule.Database_Models
{
    //Not going to change the name 
    [DisplayColumn("Client")]
    public class Client
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }

        public Address Address { get; set; }
    }
}
