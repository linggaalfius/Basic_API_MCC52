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
        public static string GetRandomSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(12);
        }

        public static string GetDate()
        {
            string time = DateTime.Now.ToString("yyyy:MM:dd H:mm");
            return time;
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
