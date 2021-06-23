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

namespace API.Repository.Data
{
    public class AccountRepository : GeneralRepository<MyContext, Account, string>
    {
        private readonly MyContext myContext;
        public AccountRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
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
            var getEmail = resetPas.Email;
            MailMessage mail = new MailMessage();
            SmtpClient smtpServer = new SmtpClient("smtp.gmail.com", 587);

            mail.From = new MailAddress("slametman69@gmail.com");
            mail.To.Add(getEmail);
            mail.Subject = "Test Reset Passw";
            mail.Body = "This is your new password: " + guid.ToString();

            smtpServer.UseDefaultCredentials = true;
            smtpServer.Credentials = new NetworkCredential("slametman69@gmail.com", "skaphobia");
            smtpServer.EnableSsl = true;
            smtpServer.Send(mail);

            var find1 = myContext.Employees.Where(e => e.Email == getEmail).FirstOrDefault<Employee>();
            var find2 = myContext.Accounts.Find(find1.NIK);
            find2.Password = BCrypt.Net.BCrypt.HashPassword(guid.ToString());
            myContext.SaveChanges();

            return 1;
        }
    }
}
