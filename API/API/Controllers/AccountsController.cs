using API.Base;
using API.Models;
using API.Repository.Data;
using API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : BaseController<Account, AccountRepository, string>
    {
        private readonly AccountRepository repository;
        public AccountsController(AccountRepository accountRepository) : base(accountRepository)
        {
            this.repository = accountRepository;
        }

        [HttpPost("Login")]
        public ActionResult Login(LoginVM loginVM)
        {
            var response = repository.Login(loginVM);
            if(response == 2)
            {
                return Ok(new { StatusCode = HttpStatusCode.OK, result = response, message = "Login Success" });
            }
            else if(response == 1)
            {
                return BadRequest(new { StatusCode = HttpStatusCode.BadRequest, result = response, message = "Password not match" });
            }
            else
            {
                return BadRequest(new { StatusCode = HttpStatusCode.BadRequest, result = response, message = "NIK / Email not found" });
            }
        }

        [HttpPost("ResetPassword")]
        public ActionResult ResetPassword(LoginVM resetPas)
        {
            var response = repository.ResetPassword(resetPas);
            if(response == 1)
            {
                return Ok(new { StatusCode = HttpStatusCode.OK, result = response, message = "Mail Sent" });
            }
            else
            {
                return BadRequest(new { StatusCode = HttpStatusCode.BadRequest, result = response, message = "Failed" });
            }
        }

        [HttpPut("ChangePassword")]
        public ActionResult ChangePassword(ChangePasswordVM changePassVM)
        {
            var response = repository.ChangePassword(changePassVM);
            if (response == 2)
            {
                return Ok(new { StatusCode = HttpStatusCode.OK, result = response, message = "Change Passw Succeed" });
            }
            else if (response == 1)
            {
                return BadRequest(new { StatusCode = HttpStatusCode.BadRequest, result = response, message = "Password not match" });
            }
            else
            {
                return BadRequest(new { StatusCode = HttpStatusCode.BadRequest, result = response, message = "NIK / Email not found" });
            }
        }
    }
}
