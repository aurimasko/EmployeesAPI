using System;
using System.IO;
using System.Threading.Tasks;

namespace EmployeesAPI.Infrastructure.Logger
{
    public class LoggerService : ILoggerService
    {
        private readonly EmployeesDbContext _context;

        public LoggerService(EmployeesDbContext context) 
        {
            _context = context;
        }

        public async Task<ErrorReport> Log(Error error)
        {
            var added = await _context.Errors.AddAsync(error).ConfigureAwait(false);

            if (added.Entity == null)
                return null;

            if (await _context.SaveChangesAsync().ConfigureAwait(false) > 0)
                return new ErrorReport(added.Entity);

            return null;
        }
    }
}
