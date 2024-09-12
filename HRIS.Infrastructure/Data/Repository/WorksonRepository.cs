using Microsoft.EntityFrameworkCore;
using HRIS.Domain.Entities;
using HRIS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Infrastructure.Data.Repository
{
    public class WorksonRepository : IWorksonRepository
    {
        private readonly HRISContext _context;

        public WorksonRepository(HRISContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Workson>> GetAllWorkson()
        {
            return await _context.Worksons.ToListAsync();
        }
        public async Task<Workson> GetWorksonById(int empNo, int projNo)
        {
            return await _context.Worksons.FindAsync(empNo, projNo);
        }
        public async Task<Workson> AddWorkson(Workson workson)
        {
            _context.Worksons.Add(workson);
            await _context.SaveChangesAsync();
            return workson;
        }
        public async Task<Workson> UpdateWorkson(Workson workson)
        {
            _context.Worksons.Update(workson);
            await _context.SaveChangesAsync();
            return workson;

        }
        public async Task<bool> DeleteWorkson(int empNo, int projNo)
        {
            var deleted = await _context.Worksons.FindAsync(empNo, projNo);
            if (deleted == null)
            {
                return false;
            }
            _context.Worksons.Remove(deleted);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<Workson>> GetWorksonByEmployee(int empNo)
        {
            return await _context.Worksons.ToListAsync();
        }

        // dashboard working hours
        public async Task<object> GetTop5EmployeesByWorkingHours()
        {
            var topEmployees = await _context.Worksons
            .GroupBy(w => w.Empno) // Group by EmployeeId
            .Select(g => new
            {
                EmployeeId = g.Key,
                TotalHoursWorked = g.Sum(w => w.Hoursworked) // Sum the working hours for each employee
            })
            .OrderByDescending(e => e.TotalHoursWorked) // Sort by total hours in descending order
            .Take(5) // Take the top 5 employees
            .Join(_context.Employees, // Join with the Employee table
                  workson => workson.EmployeeId,
                  employee => employee.Empno,
                  (workson, employee) => new
                  {
                      EmployeeId = workson.EmployeeId,
                      EmployeeName = employee.Fname + " " + employee.Lname, // Combine first and last names
                      TotalHoursWorked = workson.TotalHoursWorked
                  })
            .ToListAsync();

            return topEmployees;
        }
    }
}
