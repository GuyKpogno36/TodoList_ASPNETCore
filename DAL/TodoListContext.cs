using Microsoft.EntityFrameworkCore;
using Todo_List_ASPNETCore.Models;

namespace Todo_List_ASPNETCore.DAL
{
    public class TodoListContext : DbContext
    {
        public TodoListContext(DbContextOptions<TodoListContext> options)
            : base(options)
        {
        }

        public DbSet<TASK> TASK { get; set; }
        public DbSet<USER> USER { get; set; }
        public DbSet<CATEGORY> CATEGORY { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TASK>()
                .HasOne(t => t.Category)
                .WithMany()
                .HasForeignKey(t => t.Category_ID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TASK>()
            .Property(t => t.Task_Priority)
            .HasConversion<string>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=ASPNETCore;Trusted_Connection=True;");
        }
    }
}
