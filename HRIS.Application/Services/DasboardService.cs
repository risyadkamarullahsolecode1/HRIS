using HRIS.Application.Dtos;
using HRIS.Application.Interfaces;
using HRIS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Services
{
    public class DasboardService:IDashboardService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IWorksonRepository _worksonRepository;
    
        public DasboardService(IDepartmentRepository departmentRepository, IWorksonRepository worksonRepository)
        {
            _departmentRepository = departmentRepository;
            _worksonRepository = worksonRepository;
        }

        public async Task<KpiReportDto> GetReport()
        {
            var employee = await _departmentRepository.GetEmployeeByDepartment();
            var salary = await _departmentRepository.GetAverageSalaryByDepartmentAsync();
            var workhour = await _worksonRepository.GetTop5EmployeesByWorkingHours();

            return new KpiReportDto
            {
                GetEmployeeByDepartment = employee,
                GetAverageSalaryByDepartmentAsync = salary,
                GetTop5EmployeesByWorkingHours = workhour
            };
        }
    }
}
