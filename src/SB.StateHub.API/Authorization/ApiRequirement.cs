using Microsoft.AspNetCore.Authorization;

namespace SB.StateHub.API.Authorization
{
    public class ApiRequirement : IAuthorizationRequirement
    {
        public string Policy;

        public ApiRequirement(string policy)
        {
            Policy = policy;
        }
    }
}