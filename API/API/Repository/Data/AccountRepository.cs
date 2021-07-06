using API.Context;
using API.Models;
using API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using BCrypt.Net;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;

namespace API.Repository.Data
{
    public class AccountRepository : GeneralRepository<MyContext, Account, string>
    {
        public IConfiguration configuration;
        private readonly MyContext myContext;

        public AccountRepository(IConfiguration config, MyContext myContext) : base(myContext)
        {
            this.configuration = config;
            this.myContext = myContext;
        }
        public static string GetRandomSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(12);
        }

        public static string GetDate()
        {
            string time = DateTime.Now.ToString("yyyy:MM:dd H:mm");
            return time;
        }

        public string GenerateTokenLogin(LoginVM loginVM)
        {
            var data = (
                from account in myContext.Accounts
                join employee in myContext.Employees
                on account.NIK equals employee.NIK
                join accountRole in myContext.AccountRoles
                on account.NIK equals accountRole.NIK
                join role in myContext.Roles
                on accountRole.RoleID equals role.RoleID
                where account.NIK == $"{loginVM.NIK}" || employee.Email == $"{loginVM.Email}"
                select new
                {
                    Email = employee.Email,
                    RoleName = role.RoleName
                }).ToList();
            var claims = new List<Claim>();
            foreach (var item in data)
            {
                claims.Add(new Claim("email", item.Email));
                claims.Add(new Claim("role", item.RoleName));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(configuration["Jwt:Issuer"], configuration["Jwt:Audience"],
                claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);
            return new JwtSecurityTokenHandler().WriteToken(token);
            //return Ok(new { status = HttpStatusCode.OK, nik = user.NIK, token = show });
        }

        public int Login(LoginVM loginVM)
        {
            var login = myContext.Employees.Where(x => (x.NIK == loginVM.NIK) || (x.Email == loginVM.Email)).FirstOrDefault<Employee>();
            if(login != null)
            {
                var cekPasw = BCrypt.Net.BCrypt.Verify(loginVM.Password, login.Account.Password);
                if (cekPasw)
                {
                    return 2;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                return 0;
            }
        }

        public int ResetPassword(LoginVM resetPas)
        {
            Guid guid = Guid.NewGuid();
            var getName = myContext.Employees.FirstOrDefault(e => e.Email == resetPas.Email);
            var getEmail = resetPas.Email;
            MailMessage mail = new MailMessage();
            SmtpClient smtpServer = new SmtpClient("smtp.gmail.com", 587);

            mail.From = new MailAddress("slametman69@gmail.com");
            mail.To.Add(getEmail);
            mail.Subject = "Reset Password " + GetDate();
            mail.Body = "Hai" + getName.FirstName + ", ini passwordnya: " + guid.ToString() + ". Segera ganti passwordmu ya.";

            smtpServer.UseDefaultCredentials = true;
            smtpServer.Credentials = new NetworkCredential("slametman69@gmail.com", "skaphobia");
            smtpServer.EnableSsl = true;
            smtpServer.Send(mail);

            var find1 = myContext.Employees.Where(e => e.Email == getEmail).FirstOrDefault<Employee>();
            var find2 = myContext.Accounts.Find(find1.NIK);
            find2.Password = BCrypt.Net.BCrypt.HashPassword(guid.ToString(), GetRandomSalt());
            myContext.SaveChanges();

            return 1;
        }

        public int ChangePassword(ChangePasswordVM changePassVM)
        {
            var login = myContext.Employees.Where(x => (x.NIK == changePassVM.NIK) || (x.Email == changePassVM.Email)).FirstOrDefault<Employee>();
            if(login != null)
            {
                var checkPass = BCrypt.Net.BCrypt.Verify(changePassVM.OldPassword, login.Account.Password);
                if (checkPass)
                {
                    var changePass = myContext.Accounts.Find(login.NIK);
                    changePass.Password = BCrypt.Net.BCrypt.HashPassword(changePassVM.NewPassword, GetRandomSalt());
                    myContext.SaveChanges();
                    return 2;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                return 0;
            }
        }
    }
}
