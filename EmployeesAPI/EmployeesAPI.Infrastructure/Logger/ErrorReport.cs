using System;
namespace EmployeesAPI.Infrastructure.Logger
{
    public class ErrorReport
    {
        public ErrorReport(Error error)
        {
            Error = error;
        }

        public Guid IssueId => Error.Id;
        public Error Error { get; set; }
    }
}
