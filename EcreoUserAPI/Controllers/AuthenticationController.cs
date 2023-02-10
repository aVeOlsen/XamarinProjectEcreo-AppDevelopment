using EcreoLibraryStandard;
using EcreoUserAPI.Repositories.Common;
using EcreoUserAPI.Viewmodels.UserSignIn;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EcreoUserAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IRepository<User> _userRepository;
        public AuthenticationController(IRepository<User> repo,
                                        IConfiguration config)
        {
            _configuration = config;
            _userRepository = repo;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserSignInViewModel signInVM)
        {
            User userToFind = await _userRepository.FirstOrDefault(x => x.PersonalInformation.Email == signInVM.Email);

            if(userToFind == null)
                return NotFound();

            if(userToFind.Password != signInVM.Password)
                return BadRequest();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, userToFind.PersonalInformation.Email)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration.GetValue<string>("jwt:issuer"),
                audience: _configuration.GetValue<string>("jwt:audience"),
                claims: claims,
                expires: DateTime.Now.AddHours(double.Parse(_configuration.GetValue<string>("jwt:accessTokenExpiration"))),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("jwt:secret"))),
                                    SecurityAlgorithms.HmacSha256)
                );

            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });

        }

    }
}
