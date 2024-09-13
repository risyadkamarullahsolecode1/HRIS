using HRIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Interfaces
{
    public interface IWorkflowService
    {
        Task<IEnumerable<Process>> GetPendingProcessesForUserAsync();
    }
}
