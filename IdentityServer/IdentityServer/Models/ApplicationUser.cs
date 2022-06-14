using Microsoft.AspNetCore.Identity;

namespace IdentityServerGrocierio.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string AccountType { get; set; } = "User";
    }
}
