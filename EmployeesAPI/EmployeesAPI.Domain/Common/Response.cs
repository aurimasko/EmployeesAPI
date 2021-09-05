using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using EmployeesAPI.Domain.Configuration;

namespace EmployeesAPI.Domain.Common
{
    public class Response
    {


        protected Response()
        {
            ErrorMessages = new List<string>();
            ErrorCodes = new List<ErrorCodeTypes>();
        }

        public Response(bool isSuccess) : this()
        {
            IsSuccess = isSuccess;
        }

        public Response(string errorMessage) : this(false)
        {
            ErrorMessages.Add(errorMessage);
        }

        public Response(string errorMessage, ErrorCodeTypes errorCode) : this(errorMessage)
        {
            ErrorCodes.Add(errorCode);
        }

        public Response(IEnumerable<string> errorMessages, IEnumerable<ErrorCodeTypes> errorCodes) : this(false)
        {
            ErrorMessages.AddRange(errorMessages);
            ErrorCodes.AddRange(errorCodes);
        }

        public Response(IEnumerable<string> errorMessages) : this(false)
        {
            ErrorMessages.AddRange(errorMessages);
            ErrorCodes.Add(ErrorCodeTypes.GenericError);
        }

        public Response(Exception innerException, IEnumerable<string> errorMessages, IEnumerable<ErrorCodeTypes> errorCodesEnums) : this(errorMessages, errorCodesEnums)
        {
            InnerException = innerException;
        }


        public bool IsSuccess { get; set; }
        public List<ErrorCodeTypes> ErrorCodes { get; set; }
        public List<string> ErrorMessages { get; set; }

        [JsonIgnore]
        public Exception InnerException { get; set; }
    }

    public class Response<T> : Response
    {
        public T Content { get; set; }

        public Response() : base () { }
        public Response(bool isSuccess) : base(isSuccess) { }
        public Response(T content) : base(true) { Content = content; }

        public Response(string errorMessage) : base(errorMessage)
        {
        }

        public Response(Exception innerException, IEnumerable<string> errorMessages, IEnumerable<ErrorCodeTypes> errorCodesEnums) : base(innerException, errorMessages, errorCodesEnums)
        {
        }
    }
}
