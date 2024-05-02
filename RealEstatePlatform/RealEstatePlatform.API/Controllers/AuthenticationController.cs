using RealEstatePlatform.Application.Contracts.Identity;
using RealEstatePlatform.Application.Contracts.Interfaces;
using RealEstatePlatform.Application.Models.Identity;
using RealEstatePlatform.Identity.Models;
using Microsoft.AspNetCore.Mvc;
using RealEstatePlatform.API.Models;

namespace RealEstatePlatform.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthenticationController> _logger;
        private readonly ICurrentUserService currentUserService;

        public AuthenticationController(IAuthService authService, ILogger<AuthenticationController> logger, ICurrentUserService currentUserService)
        {
            _authService = authService;
            _logger = logger;
            this.currentUserService = currentUserService;
        }

        [HttpPut]
        [Route("update-information")]
        public async Task<IActionResult> UpdateInformation(UpdateInformationModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid payload");
                }

                var result = await _authService.UpdateInformation(model);

                if (!result)
                {
                    return BadRequest("User update failed");
                }

                return Ok("User updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid payload");
                }

                var (status, message) = await _authService.Login(model);

                if (status == 0)
                {
                    return BadRequest(message);
                }
                return Ok(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

       

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegistrationModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid payload");
                }

                var (status, message) = await _authService.Registeration(model, UserRoles.User);

                if (status == 0)
                {
                    return BadRequest(message);
                }

                return CreatedAtAction(nameof(Register), model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("register-agent")]
        public async Task<IActionResult> RegisterAgent(RegistrationModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid payload");
                }

                var (status, message) = await _authService.Registeration(model, UserRoles.Agent);

                if (status == 0)
                {
                    return BadRequest(message);
                }

                return CreatedAtAction(nameof(Register), model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await _authService.Logout();
            return Ok();
        }

        [HttpGet]
        [Route("currentuserinfo")]
        public CurrentUser CurrentUserInfo()
        {
            
            if (this.currentUserService.GetCurrentClaimsPrincipal()!=null)
            {
                return new CurrentUser
                {
                    IsAuthenticated = false
                };
            }
            return new CurrentUser
            {
                IsAuthenticated = true,

                UserName = this.currentUserService.GetCurrentUserId(),
                Claims = this.currentUserService.GetCurrentClaimsPrincipal().Claims.ToDictionary(c => c.Type, c => c.Value)
            };
        }

    }
}
