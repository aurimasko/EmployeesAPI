using System.Collections.Generic;
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

        public Response(bool isSuccess)
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

        public bool IsSuccess { get; set; }
        public List<ErrorCodeTypes> ErrorCodes { get; set; }
        public List<string> ErrorMessages { get; set; }

    }

    public class Response<T> : Response
    {
        public T Content { get; set; }

        public Response() { }
        public Response(bool isSuccess) : base(isSuccess) { }
        public Response(T content) : base(true) { Content = content; }
    }
}
