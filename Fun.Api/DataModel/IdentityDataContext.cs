using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Fun.Api.DataModel
{
    public class IdentityDataContext
    {
        public IdentityDataContext()
        {
            Users = new List<IdentityUser>();
        }

        public ICollection<IdentityUser> Users { get; }
    }
}
