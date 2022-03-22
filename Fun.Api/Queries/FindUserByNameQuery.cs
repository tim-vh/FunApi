using Fun.Api.DataModel;
using Microsoft.AspNetCore.Identity;
using Provocq;
using System.Linq;

namespace Fun.Api.Queries
{
    public class FindUserByNameQuery : IQuery<IdentityDataContext, IdentityUser>
    {

        public string NormalizedUserName { get; set; }


        public IdentityUser Execute(IdentityDataContext context)
        {
            return context.Users.FirstOrDefault(u => u.NormalizedUserName == NormalizedUserName);
        }
    }
}
