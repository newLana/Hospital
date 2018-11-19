using Microsoft.AspNet.Identity.EntityFramework;

namespace Hospital.Models.Account
{
    public class ApplicationUser : IdentityUser
    {
        public int EntityId { get; set; }
    }
}