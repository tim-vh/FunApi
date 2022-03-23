using Fun.Api.DataModel;
using Fun.Api.Identity;
using Provocq;
using System.Linq;

namespace Fun.Api.Queries
{
    public class FindUserByNameQuery : IQuery<IdentityDataContext, ApplicationUser>
    {

        public string NormalizedUserName { get; set; }


        public ApplicationUser Execute(IdentityDataContext context)
        {
            return context.Users.FirstOrDefault(u => u.NormalizedUserName == NormalizedUserName);
        }
    }
}
