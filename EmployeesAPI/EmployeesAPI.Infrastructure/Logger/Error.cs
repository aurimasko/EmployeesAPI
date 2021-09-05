using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace EmployeesAPI.Infrastructure.Logger
{
    public class Error
    {
        private Error()
        {
            TimeStamp = DateTime.UtcNow;
        }

        public Error(Exception exception, HttpRequest request) : this(exception.Message, request)
        {
            StackTrace = exception.StackTrace;
        }

        public Error(string message, HttpRequest request) : this(message)
        {
            RequestHttpHeaders = JsonConvert.SerializeObject(request.Headers);
            RequestQueryStrings = JsonConvert.SerializeObject(request.QueryString);

            try
            {
                if (request.Method == "PUT" || request.Method == "POST")
                {
                    if (request.HasFormContentType)
                    {
                        RequestBody = JsonConvert.SerializeObject(request.Form);
                    }
                    else
                    {
                        if (request.Body.Length > 0)
                        {
                            byte[] buffer = new byte[request.Body.Length];
                            var bodyReadSize = request.Body.Read(buffer, 0, (int)request.Body.Length);
                            var str = Encoding.UTF8.GetString(buffer);
                            RequestBody = JsonConvert.SerializeObject(str);
                        }
                    }
                }
            }
            catch { }

            RequestContentType = request.ContentType;
            RequestMethod = request.Method;
            RequestUrl = request.Path.ToString();

        }

        public Error(string message) : this()
        {
            Message = message;
        }

        public Error(Exception exception) : this(exception.Message)
        {
            StackTrace = exception.StackTrace;
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        public DateTime TimeStamp { get; set; }

        [Required]
        public string Message { get; set; }

        public string StackTrace { get; set; }
        public string RequestHttpHeaders { get; set; }
        public string RequestQueryStrings { get; set; }
        public string RequestMethod { get; set; }
        public string RequestUrl { get; set; }
        public string RequestBody { get; set; }
        public string RequestContentType { get; set; }
    }
}
