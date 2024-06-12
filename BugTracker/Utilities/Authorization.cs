using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BugTracker.Utilities
{
    public class Authorization : ActionFilterAttribute
    {
        private readonly string[] _roles;

        public Authorization(params string[] roles)
        {
            _roles = roles;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var role = context.HttpContext.Session.GetString("UserRole");
            if (role == null || !_roles.Contains(role))
            {
                context.Result = new RedirectToActionResult("AccessDenied", "Home", null);
            }
            base.OnActionExecuting(context);
        }
    }
}
