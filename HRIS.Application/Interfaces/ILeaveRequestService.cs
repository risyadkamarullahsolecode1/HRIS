using HRIS.Application.Dtos;
using HRIS.Application.Dtos.Account;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Interfaces
{
    public interface ILeaveRequestService
    {
        Task<BaseResponseDto> SubmitLeaveRequest(LeaveRequestDto request, IFormFile? file);
        Task<BaseResponseDto> ReviewLeaveRequest(ReviewRequestDto reviewRequest);
        Task<IEnumerable<object>> GetAllLeaveRequestStatuses();
        Task<ProcessDetailDto> GetProcessAsync(int processId);
        Task<IEnumerable<object>> GetWorkflowProcessesForFollowUp();
    }
}
