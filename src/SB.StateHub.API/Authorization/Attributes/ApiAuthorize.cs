using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using SB.StateHub.API.Authorization.Permissions.Bases;

namespace SB.StateHub.API.Authorization.Attributes
{
    public class ApiAuthorize : AuthorizeAttribute
    {
        public ApiAuthorize()
        {
            Policy = BasePermission.AUTHORIZE;
        }

        public ApiAuthorize(string policy) : base()
        {
            Policy = policy;
        }
    }
}