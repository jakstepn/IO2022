using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClientModule.Database_Models
{
    //Not going to change the name 
    [DisplayColumn("Client")]
    [Index(nameof(DbClient.Addresses))]
    public class DbClient : IdentityUser
    {
        //Id, email, phone number is already in that class - inherited from IdentityUser
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<DbAddress> Addresses { get; set; }

        public void MakeOrder(DbOrder order)
        {
            throw new NotImplementedException();
        }
        public void MakeComplaint(DbComplaint complaint)
        {
            throw new NotImplementedException();
        }
    }
}
