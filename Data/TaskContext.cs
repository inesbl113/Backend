using CLONETRELLOBACK.models;
using Microsoft.EntityFrameworkCore;

namespace CLONETRELLOBACK.Data
{
    public class TaskContext : DbContext
    {
        public TaskContext(DbContextOptions<TaskContext> options)
            : base(options) { }

        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<Projects> Projects { get; set; }
        public DbSet<Lists> Lists { get; set; }
        public DbSet<Comments> Comments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Server=tcp:trelloserveur.database.windows.net,1433;Initial Catalog=BddTrello;Persist Security Info=False;User ID=trellobdd;Password=vagep54U*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
                );
            }
        }
    }
}