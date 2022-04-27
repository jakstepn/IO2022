using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Models
{
    //Not going to change the name 
    [DisplayColumn("Client")]
    [Index(nameof(Client.Addresses))]
    public class Client : IdentityUser
    {
        //Id, email, phone number is already in that class - inherited from IdentityUser
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }

    }
}
