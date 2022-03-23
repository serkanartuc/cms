using AutoMapper;
using CMS.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CMS.Web.Areas.admin.Controllers
{
    [Area("admin")]
    public class AdminBaseController : Controller
    {

        //public override void OnActionExecuting(ActionExecutingContext context)
        //{
        //    context.ModelState.IsValid

        //    base.OnActionExecuting(context);
        //}

        public void AddNotification(string message, NotificationType type)
        {
            if (string.IsNullOrWhiteSpace(message))
                return;

            this.AddNotification(new string[] { message }, type);
        }

        public void AddNotification(string[] messages, NotificationType type)
        {
            if (TempData[$"notification.{type}"] == null)
            {
                TempData[$"notification.{type}"] = messages;
            }
            else
            {
                var notificationMessages = TempData[$"notification.{type}"] as string[];
                TempData[$"notification.{type}"] = notificationMessages.Concat(messages).ToArray();
            }
        }

        public void AddModelValidNotifications()
        {
            var errorMessages = ModelState.Values.SelectMany(x => x.Errors).Select(y => y.ErrorMessage).Where(m => !string.IsNullOrWhiteSpace(m)).ToArray();

            this.AddNotification(errorMessages, Helpers.NotificationType.Error);
        }
       
    }
}
