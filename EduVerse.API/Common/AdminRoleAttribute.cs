using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EduVerse.API.Common
{
    public class AdminRoleAttribute : Attribute,IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userClaims = context.HttpContext.RequestServices.GetService<IUserClaims>();
            var userRoles = userClaims?.GetUserRoles();
            if(userRoles == null || !userRoles.Contains("Admin"))
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
