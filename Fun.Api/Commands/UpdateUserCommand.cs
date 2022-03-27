﻿using Fun.Api.DataModel;
using Fun.Api.Identity;
using Provocq;

namespace Fun.Api.Commands
{
    public class UpdateUserCommand : ICommand<IdentityDataContext>
    {
        public ApplicationUser User { get; set; }

        public void Execute(IdentityDataContext context)
        {

        }
    }
}