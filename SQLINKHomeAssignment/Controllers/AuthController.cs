using Microsoft.AspNetCore.Mvc;
using SQLINKHomeAssignment.DTOs;
using SQLINKHomeAssignment.Services;

namespace SQLINKHomeAssignment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly IConfiguration _config;

        public AuthController(AuthService authService, IConfiguration config)
        {
            _authService = authService;
            _config = config;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _authService.LoginAsync(request);

            if (result == null)
                return Unauthorized(_config["Messages:InvalidCredentials"]);

            return Ok(result);
        }
    }
}
