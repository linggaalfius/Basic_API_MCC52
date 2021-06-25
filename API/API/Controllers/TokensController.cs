using API.Base;
using API.Context;
using API.Models;
using API.Repository.Data;
using API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
//using Microsoft.IdentityModel.JsonWebTokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokensController : ControllerBase
    {
        public IConfiguration configuration;
        private readonly MyContext myContext;

        public TokensController(IConfiguration config, MyContext context)
        {
            this.configuration = config;
            this.myContext = context;
        }

        public static bool ValidatePassword(string password, string correctHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, correctHash);
        }

        [HttpPost]
        public IActionResult Post(LoginVM loginVM)
        {
            var jwt = myContext.Employees.Where(e => e.Email == loginVM.Email).FirstOrDefault<Employee>();
            if (jwt != null)
            {
                var cekEmail = myContext.Employees.FirstOrDefault(c => c.Email == loginVM.Email);
                var user = myContext.Accounts.Find(cekEmail.NIK);
                if (user != null && ValidatePassword(loginVM.Password, user.Password))
                {
                    var email = myContext.Employees.Find(user.NIK);
                    var role = myContext.AccountRoles.FirstOrDefault(u => u.NIK == user.NIK);
                    var find = myContext.Roles.FirstOrDefault(r => r.RoleID == role.RoleID);

                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("Email", email.Email),
                        new Claim("role", find.RoleName)
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(configuration["Jwt:Issuer"], configuration["Jwt:Audience"],
                        claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);
                    var show = new JwtSecurityTokenHandler().WriteToken(token);
                    return Ok(new { status = HttpStatusCode.OK, nik = user.NIK, token = show });
                }
                else
                {
                    return BadRequest("Invalid");
                }
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
