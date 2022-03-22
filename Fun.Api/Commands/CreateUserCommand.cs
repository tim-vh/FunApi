using Fun.Api.DataModel;
using Microsoft.AspNetCore.Identity;
using Provocq;

namespace Fun.Api.Commands
{
    public class CreateUserCommand : ICommand<IdentityDataContext>
    {
        public IdentityUser User { get; set; }

        public void Execute(IdentityDataContext context)
        {
            context.Users.Add(User);
        }
    }
}
