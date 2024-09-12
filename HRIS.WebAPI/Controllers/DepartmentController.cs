using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HRIS.Domain.Entities;
using HRIS.Domain.Interfaces;
using HRIS.Infrastructure.Data.Repository;

namespace HRIS.WebAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        [HttpGet]
        public ActionResult<IQueryable<Department>> GetAllDepartment()
        {
            var department = _departmentRepository.GetAllDepartment();
            return Ok(department);
        }
        [HttpGet("{deptNo}")]
        public async Task<ActionResult<Department>> GetEmployeeById(int deptNo)
        {
            var department = await _departmentRepository.GetDepartmentById(deptNo);
            if (department == null)
            {
                return NotFound();
            }
            return Ok(department);
        }
        [HttpPost]
        public async Task<ActionResult<Department>> AddDepartment(Department department)
        {
            var createdDepartment = await _departmentRepository.AddDepartment(department);
            return Ok(createdDepartment);
        }
        [HttpPut("{deptNo}")]
        public async Task<IActionResult> UpdateEmployee(int deptNo, Department department)
        {
            if (deptNo != department.Deptno) return BadRequest();

            var updatedDepartment = await _departmentRepository.UpdateDepartment(department);
            return Ok(updatedDepartment);
        }
        [HttpDelete("{deptNo}")]
        public async Task<ActionResult<bool>> DeleteBook(int deptNo)
        {
            var deleted = await _departmentRepository.DeleteDepartment(deptNo);
            if (!deleted) return NotFound();
            return Ok("department has been deleted !");
        }
        [HttpGet("manager/{deptNo}")]
        public async Task<IActionResult> GetManagerByDeptNo(int deptNo)
        {
            var manager = await _departmentRepository.GetManagerByDeptNoAsync(deptNo);
            if (manager == null)
            {
                return NotFound($"No manager found for department number {deptNo}");
            }
            return Ok(manager);
        }
        [HttpGet("employee")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeeByDepartment()
        {
            var result = await _departmentRepository.GetEmployeeByDepartment();
            return Ok(result);
        }
        [HttpGet("salary")]
        public async Task<ActionResult<Dictionary<string, int>>> GetAverageSalaryByDepartmentAsync()
        {
            var result = await _departmentRepository.GetAverageSalaryByDepartmentAsync();
            return Ok(result);
        }
        [HttpGet("generate-employee-report")]
        public async Task<IActionResult> GenerateEmployeeReport(string departmentName, int pageNumber)
        {
            var pdfBytes = await _departmentRepository.GenerateEmployeeReportByDepartmentAsync(departmentName, pageNumber);
            return File(pdfBytes, "application/pdf", "EmployeeReport.pdf");
        }
    }
}
