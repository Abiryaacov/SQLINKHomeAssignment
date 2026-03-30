namespace SQLINKHomeAssignment.DTOs
{
    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public UserDto PersonalDetails { get; set; } = null!;
    }
}
