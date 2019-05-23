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
                    if (IsAdmin(context.User, context.Resource))
                    {
                        context.Succeed(requirement);
                    }
                }
                //else if (requirement is EditPermission ||
                //         requirement is DeletePermission)
                //{
                //    if (IsAdmin(context.User, context.Resource))
                //    {
                //        context.Succeed(requirement);
                //    }
                //}
            }

            return Task.CompletedTask;
        }

        private bool IsAdmin(ClaimsPrincipal user, object resource)
        {
            string email = authService.GetUserEmail(user);
            Entities.User dbUser = applicationContext.Users.FirstOrDefault(u => u.Email.Equals(email));
            if (dbUser != null)
            {
                if(dbUser.Role.Equals("Admin"))
                return true;
            }
            return false;
        }

        //private bool IsSponsor(ClaimsPrincipal user, object resource)
        //{

        //    return true;
        //}
    }
}
