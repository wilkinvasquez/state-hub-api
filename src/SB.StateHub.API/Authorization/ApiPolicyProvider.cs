using Microsoft.AspNetCore.Authorization;
using SB.StateHub.API.Authorization.Permissions.Bases;

namespace SB.StateHub.API.Authorization
{
    public class ApiPolicyProvider : IAuthorizationPolicyProvider
    {
        public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
        {
            return Task.FromResult<AuthorizationPolicy>(null!);
        }

        public Task<AuthorizationPolicy?> GetFallbackPolicyAsync()
        {
            return GetDefaultPolicyAsync()!;
        }

        public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            AuthorizationPolicyBuilder authorizationPolicyBuilder = new AuthorizationPolicyBuilder();
            ApiRequirement? policy = _policies.FirstOrDefault(p => p.Policy == policyName);

            if (policy != null)
            {
                authorizationPolicyBuilder.AddRequirements(policy);
            }

            return Task.FromResult(authorizationPolicyBuilder?.Build());
        }

        private readonly IEnumerable<ApiRequirement> _policies = new List<ApiRequirement>()
        {
            new ApiRequirement(BasePermission.AUTHORIZE),
            new ApiRequirement(BasePermission.ANONYMOUS)
        };
    }
}