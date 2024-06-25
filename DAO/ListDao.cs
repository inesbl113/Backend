using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using CLONETRELLOBACK.Data;
using CLONETRELLOBACK.models;

namespace CLONETRELLOBACK.Models
{
    public class ListDao
    {
        private readonly TaskContext _context;

        public ListDao(TaskContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Lists>> GetLists()
        {
            return await _context.Lists.ToListAsync();
        }

        public async Task<Lists> GetList(int id)
        {
            return await _context.Lists.FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<Lists> AddList(Lists list)
        {
            _context.Lists.Add(list);
            await _context.SaveChangesAsync();
            return list;
        }

        public async Task<Lists> UpdateList(Lists list)
        {
            _context.Entry(list).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return list;
        }

        public async Task<bool> DeleteList(int id)
        {
            var list = await _context.Lists.FindAsync(id);
            if (list == null) return false;

            _context.Lists.Remove(list);
            await _context.SaveChangesAsync();
            return true;
        }

        public bool ListExists(int id)
        {
            return _context.Lists.Any(e => e.Id == id);
        }
    }
}
