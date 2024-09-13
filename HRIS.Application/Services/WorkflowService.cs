using HRIS.Application.Interfaces;
using HRIS.Domain.Entities;
using HRIS.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Services
{
    public class WorkflowService : IWorkflowService
    {
        private readonly IWorkflowRepository _workflowRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WorkflowService(IWorkflowRepository workflowRepository, IHttpContextAccessor httpContextAccessor)
        {
            _workflowRepository = workflowRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<Process>> GetPendingProcessesForUserAsync()
        {
            // Retrieve the current user's roles from the claims
            var userRoles = _httpContextAccessor.HttpContext?.User?.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)
                .ToList();

            if (userRoles == null || !userRoles.Any())
            {
                throw new UnauthorizedAccessException("User does not have any roles assigned.");
            }

            // Fetch the current workflow steps where the user is either directly responsible or based on their roles
            var pendingProcesses = await _workflowRepository.GetPendingProcessesForUserAsync( userRoles);

            if (pendingProcesses == null || !pendingProcesses.Any())
            {
                return Enumerable.Empty<Process>();
            }

            return pendingProcesses;
        }
    }
}
