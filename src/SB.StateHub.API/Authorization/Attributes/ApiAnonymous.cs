using Microsoft.AspNetCore.Authorization;
using SB.StateHub.API.Authorization.Permissions.Bases;

namespace SB.StateHub.API.Authorization.Attributes
{
     public class ApiAnonymous : AuthorizeAttribute
    {
        public ApiAnonymous() : base()
        {
            Policy = BasePermission.ANONYMOUS;
        }
    }
}