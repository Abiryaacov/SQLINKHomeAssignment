using Microsoft.EntityFrameworkCore;
using SQLINKHomeAssignment.Models;

namespace SQLINKHomeAssignment.Services
{
    public class ProjectsService
    {
        private readonly AppDbContext _db;

        public ProjectsService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<Project>> GetProjectsByUserIdAsync(int userId)
        {
            return await _db.Projects.Where(p => p.UserId == userId).ToListAsync();
        }
    }
}
