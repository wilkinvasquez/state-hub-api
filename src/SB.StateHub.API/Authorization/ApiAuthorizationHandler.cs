using Microsoft.AspNetCore.Authorization;
using SB.StateHub.API.Authorization.Permissions.Bases;
using SB.StateHub.API.Services.Cryptography;

namespace SB.StateHub.API.Authorization
{
    public class ApiAuthorizationHandler : AuthorizationHandler<ApiRequirement>
    {
        private readonly ICryptoService _cryptoService;

        public ApiAuthorizationHandler(
            ICryptoService cryptoService
        )
        {
            _cryptoService = cryptoService;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ApiRequirement requirement)
        {
            List<IAuthorizationRequirement> requirements = context.Requirements.ToList();

            bool isAnonymous = requirements.Any(req =>
            {
                return (req as ApiRequirement)!.Policy == BasePermission.ANONYMOUS;
            });

            if (isAnonymous)
            {
                requirements.ForEach(req => context.Succeed(req));

                return Task.CompletedTask;
            }

            string? encodedToken = _cryptoService.GetEncodedToken();

            if (encodedToken == null) throw new UnauthorizedAccessException("Unauthorized");

            int? userId = null;

            userId = _cryptoService.ValidateAuthenticationToken(encodedToken!);

            if (requirement.Policy == BasePermission.AUTHORIZE)
            {
                if (userId != null)
                {
                    context.Succeed(requirement);

                    return Task.CompletedTask;
                }
            }

            throw new UnauthorizedAccessException("Unauthorized");
        }
    }
}