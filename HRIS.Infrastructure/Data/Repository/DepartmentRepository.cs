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

        /// <summary>
        /// Use  this as method to employee in each department
        /// </summary>
        /// <returns></returns>
        // dashboard total employee in each department
        public async Task<Dictionary<string, int>> GetEmployeeByDepartment()
        {
            var result = await _context.Employees
                .GroupBy(e => e.DeptnoNavigation.Deptname) 
                .Select(g => new { Deptname = g.Key, Count = g.Count() }) 
                .ToDictionaryAsync(x => x.Deptname, x => x.Count); 

            return result;
        }

        // dashboard average salary per each department
        public async Task<Dictionary<string, int>> GetAverageSalaryByDepartmentAsync()
        {
            // Group employees by Deptno (Department ID), then calculate the average salary per department
            var averageSalaries = await _context.Employees
                .GroupBy(e => e.Deptno) // Group by Department No
                .Select(g => new
                {
                    DepartmentName = g.FirstOrDefault().DeptnoNavigation.Deptname, // Get the department name
                    AverageSalary = (int)(g.Average(e => e.Salary) ?? 0) 
                })
                .ToDictionaryAsync(x => x.DepartmentName, x => x.AverageSalary);

            return averageSalaries;
        }

        // report employee by department name
        public async Task<byte[]> GenerateEmployeeReportByDepartmentAsync(string departmentName, int pageNumber)
        {
            int pageSize = 20; // Set the page size to 20 employees per page

            // Fetch all employees for the department, ordered by employee number
            var employees = await _context.Employees
                .Where(e => e.DeptnoNavigation.Deptname == departmentName)
                .OrderBy(e => e.Empno)
                .ToListAsync();

            // Start the HTML content for the PDF
            string htmlContent = "<h1>Employee Report</h1>";
            htmlContent += $"<h2>Department: {departmentName}</h2>";

            // Split the list into pages of 20 employees
            int totalPages = (int)Math.Ceiling(employees.Count / (double)pageSize);

            for (int page = 1; page <= totalPages; page++)
            {
                htmlContent += $"<h3>Page {page} of {totalPages}</h3>";
                htmlContent += "<table><thead><tr><td>Employee ID</td><td>Username</td><td>Full Name</td><td>Position</td></tr></thead><tbody>";

                var employeesPage = employees
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                employeesPage.ForEach(emp =>
                {
                    htmlContent += $"<tr><td>{emp.Empno}</td><td>{emp.Fname + " " + emp.Lname}</td><td>{emp.Fname} {emp.Lname}</td><td>{emp.Position}</td></tr>";
                });

                htmlContent += "</tbody></table>";

                // Add a page break after each page except the last one
                if (page < totalPages)
                {
                    htmlContent += "<div style='page-break-after: always;'></div>";
                }
            }

            // Generate the PDF from the HTML content
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
