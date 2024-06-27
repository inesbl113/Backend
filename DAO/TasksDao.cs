using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using CLONETRELLOBACK.Data;
using CLONETRELLOBACK.models;

namespace CLONETRELLOBACK.Models
{
    public class TasksDao
    {
        private readonly TaskContext _context;

        public TasksDao(TaskContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tasks>> GetTasks()
        {
              var tasks = await _context.Tasks
                  .Include(t => t.List) // Inclut la liste associée à chaque tâche
                  .Include(t => t.Comments) // Inclut les commentaires associés à chaque tâche
                  .ToListAsync();

                return tasks;
        }

        public async Task<Tasks> GetTaskById(int id)
{
              return await _context.Tasks // Supprimez le point après 'Tasks'
                  .Include(t => t.List)
                  .ThenInclude(l => l.Project)
                  .FirstOrDefaultAsync(t => t.Id == id);
    
}

        public async Task<Tasks> AddTask(Tasks task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<Tasks> UpdateTask(Tasks task)
        {
            _context.Entry(task).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<bool> DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return false;

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }

        public bool TaskExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}
