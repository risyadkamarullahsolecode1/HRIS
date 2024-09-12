using HRIS.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Interfaces
{
    public interface IDashboardService
    {
        Task<KpiReportDto> GetReport();
    }
}
