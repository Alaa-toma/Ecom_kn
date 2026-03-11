using KAshop.DAL.DTO.Request;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using KAshop.BLL.Service;
using IAuthenticationService = KAshop.BLL.Service.IAuthenticationService;

namespace KAshop.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly BLL.Service.IAuthenticationService _authenticationService;
        public AccountController(IAuthenticationService authenticationService) 
        { 
            _authenticationService = authenticationService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisteAsync(RegisterRequest request)
        {
            var result = await _authenticationService.RegisterAsync(request);
            return Ok(result);
        }


        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginRequest request)
        {
            var result = await _authenticationService.LoginAsync(request);


            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }


        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string token, string UserId)
        {
            var isConfirmed = _authenticationService.ConfirmEmailAsync(token, UserId);

            if (isConfirmed.IsCompletedSuccessfully) { return Ok(); }
            return BadRequest();
        }

    }
}
