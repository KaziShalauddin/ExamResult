﻿using System;
using System.Web;
using System.Web.Mvc;

namespace Exam_Result
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new ErrorFilter());
            //filters.Add(new SessionFilter());
        }
    }

    public class ErrorFilter : System.Web.Mvc.HandleErrorAttribute
    {
        public override void OnException(System.Web.Mvc.ExceptionContext filterContext)
        {
            System.Exception exception = filterContext.Exception;
            string controller = filterContext.RouteData.Values["controller"].ToString(); ;
            string action = filterContext.RouteData.Values["action"].ToString();

            if (filterContext.ExceptionHandled)
            {
                return;
            }
            else
            {
                // Determine the return type of the action
                string actionName = filterContext.RouteData.Values["action"].ToString();
                Type controllerType = filterContext.Controller.GetType();
                var method = controllerType.GetMethod(actionName);
                var returnType = method.ReturnType;

                // If the action that generated the exception returns JSON
                if (returnType.Equals(typeof(JsonResult)))
                {
                    filterContext.Result = new JsonResult()
                    {
                        Data = "DATA not returned"
                    };
                }

                // If the action that generated the exception returns a view
                if (returnType.Equals(typeof(ActionResult))
                    || (returnType).IsSubclassOf(typeof(ActionResult)))
                {
                    filterContext.Result = new ViewResult
                    {
                        ViewName = "Error"
                    };
                }
            }

            // Make sure that we mark the exception as handled
            filterContext.ExceptionHandled = true;
        }
    }

}
