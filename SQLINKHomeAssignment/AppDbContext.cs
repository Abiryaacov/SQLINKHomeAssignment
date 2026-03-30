using Microsoft.EntityFrameworkCore;
using SQLINKHomeAssignment.Models;

namespace SQLINKHomeAssignment
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
    }
}