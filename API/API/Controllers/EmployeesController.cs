using API.Base;
using API.Context;
using API.Models;
using API.Repository.Data;
using API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository repository;
        public EmployeesController(EmployeeRepository employeeRepository) : base(employeeRepository)
        {
            this.repository = employeeRepository;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public ActionResult Register(RegisterVM registerVM)
        {
            var regis = repository.Register(registerVM);
            if (regis == 2)
            {
                return Ok(new { StatusCode = HttpStatusCode.OK, result = regis, message = "Register Success" });
            }
            else if (regis == 1)
            {
                return BadRequest(new { StatusCode = HttpStatusCode.BadRequest, result = regis, message = "Email already exist" });
            }
            else
            {
                return BadRequest(new { StatusCode = HttpStatusCode.BadRequest, result = regis, message = "NIK already exist" });
            }
        }

        //[Authorize]
        [HttpGet("ViewRegistered")]
        public ActionResult ViewRegistered()
        {
            var view = repository.ViewRegistered();
            if (view != null)
            {
                return Ok(new { StatusCode = HttpStatusCode.OK, result = view, message = "View Success" });
            }
            else
            {
                return BadRequest(new { StatusCode = HttpStatusCode.BadRequest, result = view, message = "View not success" });
            }
        }

        [HttpGet("ViewByID/{nik}")]
        public ActionResult ViewRegisteredbyID(string nik)
        {
            var view = repository.ViewRegisteredbyID(nik);
            if (view != null)
            {
                return Ok(new { StatusCode = HttpStatusCode.OK, result = view, message = "View Success" });
            }
            else
            {
                return BadRequest(new { StatusCode = HttpStatusCode.BadRequest, result = view, message = "View not success" });
            }
        }
    }
}
