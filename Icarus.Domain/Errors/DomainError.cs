using Icarus.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Icarus.Domain.Errors
{
    public static class DomainError
    {
        public static class Member
        {
            public static readonly Error EmailAlreadyInUse = new Error(
                "Member.EmailAlreadyInUse",
                "Email already in use");

            public static readonly Error InvalidCredentials = new Error(
                "Member.InvalidCredentials",
                "InvalidCredentials");
        }

    }
}
