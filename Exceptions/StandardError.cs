using System;
using System.Net;
using Microsoft.AspNetCore.Http;

namespace ApiUser.Exceptions
{

    public class StandardError
    {
        public StandardError(DateTime timestamp,HttpStatusCode status, string error, string message, string path)
        {
            this.Timestamp = timestamp;
            this.status = status;
            this.Error = error;
            this.Message = message;
            this.Path = path;
        }

        
        public DateTime Timestamp { get; set; }
        public HttpStatusCode status { get; set;}
        public string Error { get; set; }
        public string Message { get; set; }
        public string Path { get; set; }
    }
}