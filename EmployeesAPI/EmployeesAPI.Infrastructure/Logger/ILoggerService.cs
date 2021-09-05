using System;
using System.Threading.Tasks;

namespace EmployeesAPI.Infrastructure.Logger
{
    public interface ILoggerService
    {
        Task<ErrorReport> Log(Error error);
    }
}
