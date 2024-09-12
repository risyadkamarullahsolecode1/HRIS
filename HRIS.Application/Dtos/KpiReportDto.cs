using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Dtos
{
    public class KpiReportDto
    {
        public Dictionary<string, int> GetAverageSalaryByDepartmentAsync {  get; set; }
        public Dictionary<string, int> GetEmployeeByDepartment {  get; set; }
        public object GetTop5EmployeesByWorkingHours {  get; set; }
    }
}
