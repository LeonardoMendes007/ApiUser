using ApiUser.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace ApiUser.Controllers
{
    public static class ControleExtensionMethods
    {
        public static ActionResult NotFound(this ControllerBase controller, string message, Microsoft.AspNetCore.Http.HttpRequest req)
        {

            return controller.NotFound(new StandardError(DateTime.UtcNow, HttpStatusCode.NotFound, "Resource Not Found", message, req.Path));
        }

        public static ActionResult BadRequest(this ControllerBase controller, string message, Microsoft.AspNetCore.Http.HttpRequest req)
        {

            return controller.NotFound(new StandardError(DateTime.UtcNow, HttpStatusCode.BadRequest, "Bad Request", message, req.Path));
        }

        public static ActionResult InternalServerError(this ControllerBase controller, string message, Microsoft.AspNetCore.Http.HttpRequest req)
        {
            return controller.StatusCode((int)HttpStatusCode.InternalServerError,new StandardError(DateTime.UtcNow, HttpStatusCode.InternalServerError, "Internal Server Error", message, req.Path));
        }
    }
}
