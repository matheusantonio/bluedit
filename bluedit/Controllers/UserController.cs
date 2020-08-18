using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using AspNetCore.Identity.Mongo.Model;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Claims;
using System;
using System.Text;
using System.Linq;
using bluedit.Models.ViewModel.Users;

namespace bluedit.Controllers
{
    [Route("bluedit/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<MongoUser> _userManager;
        private readonly SignInManager<MongoUser> _signInManager;
        private readonly AppSettings _appSettings;

        public UserController(
            UserManager<MongoUser> userManager,
            SignInManager<MongoUser> signInManager,
            IOptions<AppSettings> appSettings
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appSettings = appSettings.Value;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterUserViewModel registerUser)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(e => e.Errors)); 

            var user = new MongoUser{
                UserName = registerUser.UserName,
                Email = registerUser.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, registerUser.Password);

            if(!result.Succeeded) return BadRequest(result.Errors);

            await _signInManager.SignInAsync(user, false);

            return Ok(new {
                token = await GenerateJwt(registerUser.UserName),
                user = registerUser.UserName
            });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUserViewModel loginUser)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

            var result = await _signInManager.PasswordSignInAsync(loginUser.UserName, loginUser.Password, false, false);

            if(result.Succeeded)
            {
                return Ok(new {
                    token = await GenerateJwt(loginUser.UserName),
                    user = loginUser.UserName
                });
            }

            return BadRequest("Usuário ou senha inválidos");

        }

        [Authorize]
        [HttpGet("Current")]
        public async Task<IActionResult> Current()
        {
            var userName = HttpContext.User.FindFirstValue("username");
            var user = await _userManager.FindByNameAsync(userName);

            return Ok(
                new CurrentUserViewModel{
                    UserName = user.UserName,
                    Email = user.Email
                }
            );
        }

        private async Task<string> GenerateJwt(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            var claims = new List<Claim>();
            claims.Add(new Claim("username", userName));

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var token = new JwtSecurityToken(
                issuer: _appSettings.Emissor,
                audience: _appSettings.ValidIn,
                expires: DateTime.UtcNow.AddHours(_appSettings.ExpireIn),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                claims: claims);

            return tokenHandler.WriteToken(token);
        }
    }
}