using MCC.Models;
using MCC.Data.Dtos;
using Microsoft.EntityFrameworkCore;
namespace MCC.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Grade> Grades { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<ValidationDto> ValidationDto { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ValidationDto>().HasNoKey();
            base.OnModelCreating(modelBuilder);
        }


    }
}
