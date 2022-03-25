using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Fun.Api.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsEnabled { get; set; }

        public List<string> Roles { get; set; } = new List<string>();

        public ApplicationUser()
        {

        }
        
        public ApplicationUser(string userName) : base(userName)
        {
            
        }
    }
}
