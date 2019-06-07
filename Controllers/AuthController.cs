using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.Data.Model;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace WebApplication4.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {

        private UserManager<ApplicationUser> userManager;
        public IConfiguration Configuration { get; }


        public AuthController(UserManager<ApplicationUser> userManager , IConfiguration configuration)
        {
            this.userManager = userManager;
            this.Configuration = configuration;  
        }



        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model  )
        {
            var user = await userManager.FindByNameAsync(model.UserName);
            var claims = new[]
            {
                new Claim (JwtRegisteredClaimNames.Sub,user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,new Guid().ToString())
            };

            string key = Configuration.GetValue(typeof(string), "jwtTokenSecurityKey").ToString(); 
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));


            if (user != null && await userManager.CheckPasswordAsync(user,model.Password))
            {
                var token = new JwtSecurityToken(
                    issuer: "http://myHost.com",
                    audience: "http://myhost.com",
                    expires:DateTime.UtcNow.AddHours(1),
                    claims:claims,
                    signingCredentials : new Microsoft.IdentityModel.Tokens.SigningCredentials(signingKey,SecurityAlgorithms.HmacSha256)
                    );
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }

            return Unauthorized();
        }
    }
}