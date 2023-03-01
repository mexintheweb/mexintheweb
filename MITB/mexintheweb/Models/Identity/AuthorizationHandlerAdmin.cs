using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace mexintheweb.Models.Identity
{
    public class AuthorizationHandlerAdmin : AuthorizationHandler<OperationAuthorizationRequirement, IdentityUser>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, IdentityUser resource)
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
