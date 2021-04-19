
using System;
using System.Net;

namespace ApiUser.Exceptions
{

    public static class CustomErrors
    {
       public static StandardError NotFound(string message, Microsoft.AspNetCore.Http.HttpRequest req){
          
          return new StandardError(DateTime.Now,HttpStatusCode.NotFound,"Resource Not Found",message,req.Path);
       }

       public static StandardError BadRequest(string message, Microsoft.AspNetCore.Http.HttpRequest req){
          
          return new StandardError(DateTime.Now,HttpStatusCode.BadRequest,"Internal error",message,req.Path);
       }
    }
}