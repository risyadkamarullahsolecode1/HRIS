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
    public class ProjectRepository : IProjectRepository
    {
        private readonly HRISContext _context;

        public ProjectRepository(HRISContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Project>> GetAllProject()
        {
            return await _context.Projects.ToListAsync();
        }
        public async Task<Project> GetProjectById(int projNo)
        {
            return await _context.Projects.FindAsync(projNo);
        }
        public async Task<Project> AddProject(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
            return project;
        }
        public async Task<Project> UpdateProject(Project project)
        {
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
            return project;
        }
        public async Task<bool> DeleteProject(int projNo)
        {
            var deleted = await _context.Projects.FindAsync(projNo);
            if (deleted == null)
            {
                return false;
            }
            _context.Projects.Remove(deleted);
            await _context.SaveChangesAsync();
            return true;
        }

        // report for project report
        public async Task<byte[]> GenerateProjectReportAsync()
        {
            var projects = await _context.Projects
                .Select(p => new
                {
                    ProjectName = p.Projname,
                    DepartmentName = p.DeptnoNavigation.Deptname,
                    TotalHours = p.Worksons.Sum(w => w.Hoursworked),
                    TotalEmployees = p.Worksons.Select(w => w.Empno).Distinct().Count(),
                    AverageHours = p.Worksons.Average(w => w.Hoursworked)
                })
                .ToListAsync();

            string htmlContent = "<h1>Project Report</h1>";
            htmlContent += "<table><thead><tr><td>Project Name</td><td>Department Name</td><td>Total Hours</td><td>Total Employees</td><td>Average Hours</td></tr></thead><tbody>";

            projects.ForEach(project =>
            {
                htmlContent += $"<tr><td>{project.ProjectName}</td><td>{project.DepartmentName}</td><td>{project.TotalHours}</td><td>{project.TotalEmployees}</td><td>{project.AverageHours}</td></tr>";
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
