using HRIS.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRIS.WebAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [Authorize(Roles = "HR Manager, Administrator")]
        [HttpGet]
        public async Task<IActionResult> GetKpiReport()
        {
            var result = await _dashboardService.GetReport();
            return Ok(result);
        }
    }
}
