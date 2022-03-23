using Microsoft.AspNetCore.Identity;

namespace Fun.Api.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsEnabled { get; set; }

        public ApplicationUser()
        {

        }
        
        public ApplicationUser(string userName) : base(userName)
        {
            
        }
    }
}
