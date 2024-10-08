﻿using Microsoft.EntityFrameworkCore;
using HRIS.Domain.Entities;
using HRIS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Infrastructure.Data.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly HRISContext _context;

        public EmployeeRepository(HRISContext context)
        {
            _context = context;
        }
        public IQueryable<Employee> GetAllEmployee()
        {
            var employee = _context.Employees.AsQueryable();
            return employee;
        }

        public async Task<Employee> GetEmployeeById(int empNo)
        {
            return await _context.Employees.FindAsync(empNo);
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
            return employee;
        }
        public async Task<Employee> UpdateEmployeeAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
            return employee;
        }
        public async Task<bool> DeleteEmployee(int empNo)
        {
            var employee = await _context.Employees.FindAsync(empNo);
            if (employee == null)
            {
                return false;
            }
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
