using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CLONETRELLOBACK.Data;
using CLONETRELLOBACK.models;


using CLONETRELLOBACK.Models;

namespace CLONETRELLOBACK.Models
{
    public class CommentDao
    {
        private readonly TaskContext _context;

        public CommentDao(TaskContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Comments>> GetComments()
        {
            return await _context.Comments
                         .Include(c => c.Task)
                         .ThenInclude(t => t.List)
                         .ThenInclude(c => c.Project)
                         .ToListAsync();
        }

        public async Task<Comments> GetComment(int id)
        {
            return await _context.Comments
                         .Include(c => c.Task)
                         .ThenInclude(t => t.List)
                         .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Comments> AddComment(Comments comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comments> UpdateComment(Comments comment)
        {
            _context.Entry(comment).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<bool> DeleteComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null) return false;

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return true;
        }

        public bool CommentExists(int id)
        {
            return _context.Comments.Any(e => e.Id == id);
        }
    }
}