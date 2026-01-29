using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoApp.API.DTOs;
using TodoApp.API.Notifications;
using TodoApp.API.UseCases;

namespace TodoApp.API.Controllers
{
    
    [ApiController]
    [Route("api/auth")]
    public class AuthController(IUserService userService, DomainNotifier notifier) : ControllerBase
    {
        private readonly IUserService _userService = userService;
        private readonly DomainNotifier _notifier = notifier;

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserCreateDto userDto)
        {
            var result = await _userService.RegisterAsync(userDto);

            if(_notifier.HasNotifications)
                return BadRequest(_notifier.Notifications);

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userDto)
        {
            var result = await _userService.LoginAsync(userDto);

            if (_notifier.HasNotifications)
            {
                if(_notifier.Notifications.Any(n => n.Key == "UserNotFound"))
                    return NotFound(_notifier.Notifications);

                return Unauthorized(_notifier.Notifications);
            }

            return Ok(result);
        }
    }
}
