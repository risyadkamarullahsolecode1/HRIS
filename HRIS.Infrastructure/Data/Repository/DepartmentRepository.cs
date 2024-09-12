using Microsoft.EntityFrameworkCore;
using HRIS.Domain.Entities;
using HRIS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSharpCore.Pdf;
using PdfSharpCore;
using TheArtOfDev.HtmlRenderer.Core;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace HRIS.Infrastructure.Data.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly HRISContext _context;
        public DepartmentRepository(HRISContext context)
        {
            _context = context;
        }

        public IQueryable<Department> GetAllDepartment()
        {
            var department = _context.Departments.AsQueryable();
            return department;
        }
        public async Task<Department> GetDepartmentById(int deptNo)
        {
            return await _context.Departments.FindAsync(deptNo);
        }
        public async Task<Department> AddDepartment(Department department)
        {
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();
            return department;
        }
        public async Task<Department> UpdateDepartment(Department department)
        {
            _context.Departments.Update(department);
            await _context.SaveChangesAsync();
            return department;
        }
        public async Task<bool> DeleteDepartment(int deptNo)
        {
            var department = await _context.Departments.FindAsync(deptNo);
            if (department == null)
            {
                return false;
            }

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<Employee> GetManagerByDeptNoAsync(int deptNo)
        {
            // Get the department by department number
            var department = await _context.Departments
                .Where(d => d.Deptno == deptNo)
                .Select(d => d.Mgrempno) // Assuming Mgrempno is the manager's employee ID
                .FirstOrDefaultAsync();

            if (department == null)
                return null;

            // Get the manager’s details using the employee ID
            return await _context.Employees
                .Where(e => e.Empno == department)
                .FirstOrDefaultAsync();
        }

        public async Task<Employee> GetSupervisorByDeptNoAsync(int deptNo)
        {
            var department = await _context.Departments
                .Where(d => d.Deptno == deptNo)
                .Select(d => d.Spvempno)
                .FirstOrDefaultAsync();

            if (department == null)
                return null;

            return await _context.Employees
                .Where(e => e.Empno == department)
                .FirstOrDefaultAsync();
        }
        public async Task<List<Employee>> GetEmployeesBySupervisorIdAsync(int spvEmpNo)
        {
            // Find the department number where the given supervisor is assigned
            var department = await _context.Departments
                .Where(d => d.Spvempno == spvEmpNo)
                .Select(d => new { d.Deptno, d.Spvempno })
                .FirstOrDefaultAsync();

            var manager = await _context.Departments
                .Where(d => d.Mgrempno == spvEmpNo)
                .Select(d => new { d.Deptno, d.Mgrempno })  // Include department number and manager number
                .FirstOrDefaultAsync();

            if (department == null)
            {
                throw new KeyNotFoundException($"Department with supervisor No {spvEmpNo} not found.");
            }

            // Get employees within the department, excluding the supervisor and manager
            var employees = await _context.Employees
                .Where(e => e.Deptno == department.Deptno && e.Empno != spvEmpNo)  // Exclude the supervisor themselves
                .ToListAsync();

            return employees;
        }
        public async Task<Dictionary<string, int>> GetEmployeeByDepartment()
        {
            var result = await _context.Employees
                .GroupBy(e => e.DeptnoNavigation.Deptname) // Group by the department name
                .Select(g => new { Deptname = g.Key, Count = g.Count() }) // Select department name and count of employees
                .ToDictionaryAsync(x => x.Deptname, x => x.Count); // Convert to dictionary

            return result;
        }

        public async Task<Dictionary<string, int>> GetAverageSalaryByDepartmentAsync()
        {
            // Group employees by Deptno (Department ID), then calculate the average salary per department
            var averageSalaries = await _context.Employees
                .GroupBy(e => e.Deptno) // Group by Department ID
                .Select(g => new
                {
                    DepartmentName = g.FirstOrDefault().DeptnoNavigation.Deptname, // Get the department name
                    AverageSalary = (int)(g.Average(e => e.Salary) ?? 0) // Cast average salary to int, fallback to 0 if null
                })
                .ToDictionaryAsync(x => x.DepartmentName, x => x.AverageSalary);

            return averageSalaries;
        }

        public async Task<byte[]> GenerateEmployeeReportByDepartmentAsync(string departmentName, int pageNumber)
        {
            int pageSize = 20; // Limit to 20 employees per page

            var employees = await _context.Employees
                .Where(e => e.DeptnoNavigation.Deptname == departmentName)
                .OrderBy(e => e.Fname + " " + e.Lname)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            string htmlContent = "<h1>Employee Report</h1>";
            htmlContent += $"<h2>Department: {departmentName}</h2>";
            htmlContent += "<table><thead><tr><td>Employee ID</td><td>Username</td><td>Full Name</td></tr></thead><tbody>";

            employees.ForEach(emp =>
            {
                htmlContent += $"<tr><td>{emp.Empno}</td><td>{emp.Fname + " " + emp.Lname}</td><td>{emp.Fname} {emp.Lname}</td></tr>";
            });

            htmlContent += "</tbody></table>";

            return GeneratePdf(htmlContent);
        }

        private byte[] GeneratePdf(string htmlContent)
        {
            var document = new PdfDocument();
            var config = new PdfGenerateConfig
            {
                PageOrientation = PageOrientation.Portrait,
                PageSize = PageSize.A4
            };

            string cssStr = File.ReadAllText(@"./Templates/ReportTemplates/style.css");
            CssData css = PdfGenerator.ParseStyleSheet(cssStr);

            PdfGenerator.AddPdfPages(document, htmlContent, config, css);

            using (var stream = new MemoryStream())
            {
                document.Save(stream, false);
                return stream.ToArray();
            }
        }
    }
}
