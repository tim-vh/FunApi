using Fun.Api.Identity;
using System.Collections.Generic;

namespace Fun.Api.DataModel
{
    public class IdentityDataContext
    {
        public IdentityDataContext()
        {
            Users = new List<ApplicationUser>();
        }

        public ICollection<ApplicationUser> Users { get; set; }
    }
}
