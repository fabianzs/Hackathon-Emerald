using Microsoft.AspNetCore.Authorization;
using OnlineQueuing.Data;
using OnlineQueuing.Entities;
using OnlineQueuing.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnlineQueuing.Helpers
{
    public class PermissionHandler : IAuthorizationHandler
    {
        private readonly ApplicationContext applicationContext;
        private readonly IAuthService authService;

        public PermissionHandler(ApplicationContext applicationContext, IAuthService authService)
        {
            this.authService = authService;
            this.applicationContext = applicationContext;
        }

        public Task HandleAsync(AuthorizationHandlerContext context)
        {
            var pendingRequirements = context.PendingRequirements.ToList();

            foreach (var requirement in pendingRequirements)
            {
                if (requirement is AdminRequirement)
                {
                    if (IsAdmin(context.User))
                    {
                        context.Succeed(requirement);
                    }
                }
            }

            return Task.CompletedTask;
        }

        private bool IsAdmin(ClaimsPrincipal user)
        {
            string role = user.Identities.FirstOrDefault().Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value;
            if(role.Equals("Admin"))
            { 
                return true;
            }
            return false;
        }
    }
}
