using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Icarus.Presentation.Routes;

public static class ApiRoutes
{
    public static class Authentication
    {
        public const string Login = "account/login";
        public const string Register = "account/register";
    }

    public static class Member
    {
        public const string GetMemberById = "api/members/{id:guid}";
        public const string GetMembers = "api/members";
        public const string UpdateMember = "api/members";
        public const string DeleteMember = "api/members/{id:guid}";
    }
}
