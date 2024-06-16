using Microsoft.AspNetCore.Authorization;
using modLib.BL;
using modLib.Entities.Constants;
using modLib.Entities.DTO.Auth;

namespace modLib.Handlers
{
    public class PermissionAuthorizationHandler 
        : AuthorizationHandler<PermissionRequirement>
    {
        IServiceScopeFactory _serviceScopeFactory;

        public PermissionAuthorizationHandler(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context, 
            PermissionRequirement requirement)
        {
            var userIdClaim = context.User.Claims.FirstOrDefault(c => c.Type == CustomClaims.UserId);
            if (userIdClaim == null)
                return;

            var userId = Convert.ToInt32(userIdClaim.Value);

            using var scope = _serviceScopeFactory.CreateScope();

            var userService = scope.ServiceProvider.GetRequiredService<UserService>();

            var permissions = await userService.GetPermissionsAsync(userId);

            if (permissions.Intersect(requirement.Permissions).Any())
                context.Succeed(requirement);
        }
    }
}
