using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Artsgram.Filters
{
    public class SessionCheckAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
          
            int? userId = context.HttpContext.Session.GetInt32("UserId");
         
            if (userId == null)
            {
             
                context.Result = new RedirectToActionResult("Index", "Home", null);
            }
        }
    }
}