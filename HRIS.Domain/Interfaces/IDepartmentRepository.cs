using HRIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Domain.Interfaces
{
    public interface IDepartmentRepository
    {
        IQueryable<Department> GetAllDepartment();
        Task<Department> GetDepartmentById(int deptNo);
        Task<Department> AddDepartment(Department department);
        Task<Department> UpdateDepartment(Department department);
        Task<bool> DeleteDepartment(int deptNo);
        Task<Employee> GetManagerByDeptNoAsync(int deptNo);
        Task<Employee> GetSupervisorByDeptNoAsync(int deptNo);
        Task<List<Employee>> GetEmployeesBySupervisorIdAsync(int spvEmpNo);

        // dashboard total employee in each department
        Task<Dictionary<string, int>> GetEmployeeByDepartment();

        // dashboard average salary per each department
        Task<Dictionary<string, int>> GetAverageSalaryByDepartmentAsync();

        // report employee by department name
        Task<byte[]> GenerateEmployeeReportByDepartmentAsync(string departmentName, int pageNumber);
    }
}
