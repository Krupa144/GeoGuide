using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using Supabase.Gotrue;
using GeoTipsBackend.Models.Dtos.Auth;
using GeoTipsBackend.Models.Dtos.User;

namespace GeoTipsBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;

        public AuthController(ILogger<AuthController> logger)
        {
            _logger = logger;
        }



        [HttpGet("protected-data")]
        [Authorize]
        public IActionResult GetProtectedData()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

            _logger.LogInformation("Accessed protected data by user ID: {UserId}, Email: {UserEmail}", userId, userEmail);

            return Ok(new { Message = "You have access to protected data!", UserId = userId, UserEmail = userEmail });
        }

    }
}