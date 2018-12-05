using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using GroupProjectBackend.Models.DB;
using GroupProjectBackend.Models.Dto;

namespace GroupProjectBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(UserRegistrationModelDto model)
        {
            try
            {
                if (_signInManager.IsSignedIn(User))
                {
                    return BadRequest("Already logged in");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var user = new ApplicationUser
                {
                    UserName = model.Username,
                    Name = model.Name,
                    Surname = model.Surname
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return Ok("Registered");
                }
                return BadRequest(result.Errors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(UserLoginModelDto model)
        {
            try
            {
                if (_signInManager.IsSignedIn(User))
                {
                    return BadRequest("Already logged in");
                }
                var loginResult = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);
                if (loginResult.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(model.Username);
                    var result = new UserRegistrationModelDto
                    {
                        Id = user.Id,
                        Username = user.UserName,
                        Name = user.Name,
                        Surname = user.Surname
                    };
                    return Ok(result);
                }
                return BadRequest("Couldn't log in");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        [HttpGet]
        [Route("IsAuthenticated")]
        public async Task<IActionResult> IsAuthenticated()
        {
            var user = await _userManager.FindByIdAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = new UserRegistrationModelDto
            {
                Id = user.Id,
                Username = user.UserName,
                Name = user.Name,
                Surname = user.Surname
            };
            return Ok(result);
        }
    }
}
