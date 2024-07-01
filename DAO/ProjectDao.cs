using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using CLONETRELLOBACK.Data;
using CLONETRELLOBACK.models;

namespace CLONETRELLOBACK.Models
{
    public class ProjectDao
    {
        private readonly TaskContext _context;

        public ProjectDao(TaskContext context)
        {
            _context = context;
        }

    public async Task<IEnumerable<Projects>> GetProjects()
{
    return await _context.Projects
        .Include(p => p.Lists)
        .ThenInclude(l => l.Tasks) 
        .ThenInclude(t => t.Comments) // Corrigé ici
        .ToListAsync();
}
    
        public async Task<Projects> GetProject(int id)
        {
            return await _context.Projects.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Projects> AddProject(Projects project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task<Projects> UpdateProject(Projects project)
        {
            _context.Entry(project).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task<bool> DeleteProject(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null) return false;

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return true;
        }

        public bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }
    }
}
