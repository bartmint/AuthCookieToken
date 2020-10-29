using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auth2._0.Database;
using Auth2._0.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Auth2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly JwtTokenCreator _jwtCreator;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(ILogger<AccountController> logger, JwtTokenCreator jwtCreator, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _jwtCreator = jwtCreator;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpPost("login")]
        public async Task<ActionResult> LoginApi([FromBody] LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var userr =await _userManager.FindByEmailAsync(model.Email);
                var signIn = await _signInManager.CheckPasswordSignInAsync(userr, model.Password, false);

                if (signIn.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    var token = _jwtCreator.Generate(user.Email, user.Id);


                    await _userManager.UpdateAsync(user);

                    Response.Cookies.Append("X-Access-Token", token, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
                    Response.Cookies.Append("X-Username", user.UserName, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });

                    return Ok();
                }
                else
                {
                    return BadRequest(new { signIn.IsLockedOut, signIn.IsNotAllowed, signIn.RequiresTwoFactor });
                }
            }
            else
                return BadRequest(ModelState);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public string Get()
        {
            return "You are authenticated";
        }
    }

    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
