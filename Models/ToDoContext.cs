using Microsoft.EntityFrameworkCore;

namespace ToDoApp.Models
{
    public class ToDoContext : DbContext
    {

        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options)
        { }
            public DbSet<ToDo> ToDoSet { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Status> StatusSet { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = "home", Name = "Home" },
                new Category { CategoryId = "work", Name = "Work" },
                new Category { CategoryId = "hobby", Name = "Hobby" }
            );

            modelBuilder.Entity<Status>().HasData(
                new Status { StatusId = "open", Name = "Open" },
                new Status { StatusId = "done", Name = "Done" }
            );
        } 
        
    }
}
