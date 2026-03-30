namespace SQLINKHomeAssignment.DTOs
{
    public class UserDto
    {
        public string Name { get; set; } = string.Empty;
        public string Team { get; set; } = string.Empty;
        public DateTime JoinedAt { get; set; }
        public string? Avatar { get; set; }
    }
}
