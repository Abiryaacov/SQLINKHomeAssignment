namespace SQLINKHomeAssignment.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Team { get; set; } = string.Empty;
        public DateTime JoinedAt { get; set; }
        public string? Avatar { get; set; }
        public List<Project> Projects { get; set; } = new();
    }
}
