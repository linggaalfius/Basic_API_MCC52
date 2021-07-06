using API.Models;
using API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewClient.Base;
using NewClient.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewClient.Controllers
{
    public class LoginController : Controller
    {
        private readonly LoginRepository repository;

        public LoginController(LoginRepository repository)
        {
            this.repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Auth(LoginVM login)
        {
            var jwToken = await repository.Auth(login);
            if (jwToken == null)
            {
                return RedirectToAction("index");
            }

            HttpContext.Session.SetString("JWToken", jwToken.Token);
            //HttpContext.Session.SetString("Nama", jwToken.Name);
            return RedirectToAction("index", "home");
        }
    }
}
