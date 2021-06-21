using API.Models;
using API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    //public class OldEmployeesController : ControllerBase
    //{
    //    private EmployeeRepository employeeRepository;

    //    public OldEmployeesController(EmployeeRepository employeeRepository)
    //    {
    //        this.employeeRepository = employeeRepository;
    //    }

    //    [HttpGet]
    //    public ActionResult Get()
    //    {
    //        return Ok(employeeRepository.Get());
    //    }

    //    [HttpGet("{nik}")] //beda dari atas, cek nik
    //    public ActionResult Get(string nik)
    //    {
    //    var get = employeeRepository.Get(nik);
    //        if(get == null)
    //        {
    //            return Ok(new { status = HttpStatusCode.OK, result = get, message = "Tidak ditemukan" });
    //        }
    //        else
    //        {
    //            return Ok(new { status = HttpStatusCode.OK, result = get, message = "Data ditemukan" });
    //        }
    //    }

    //    [HttpPost]
    //    public ActionResult Post(Employee employee)
    //    {
    //        var insert = employeeRepository.Insert(employee);
    //        if (insert == 1)
    //        {
    //            return Ok("Insert Berhasil");
    //        }
    //        else
    //        {
    //            return BadRequest("Gagal Insert");
    //        }
    //    }

    //    [HttpDelete]
    //    public ActionResult Delete(string Nik)
    //    {
    //        var response = employeeRepository.Delete(Nik);
    //        if (Nik == null)
    //        {
    //            return BadRequest("Butuh NIK");
    //        }
    //        else
    //        {
    //            if (response == 1)
    //            {
    //                return Ok("Hapus berhasl");
    //            }
    //            else
    //            {
    //                return Ok("Hapus gagal");
    //            }
    //        }
    //    }

    //    [HttpPut]
    //    public ActionResult Put(Employee employee, string nik)
    //    {
    //        var response = employeeRepository.Update(employee, nik);
    //        if (nik == null)
    //        {
    //            return BadRequest("Butuh Nik");
    //        }
    //        else
    //        {
    //            if(response == 1)
    //            {
    //                return Ok("Update berhasil");
    //            }
    //            else
    //            {
    //                return Ok("Update gagal");
    //            }
    //        }
    //    }

    //}
}
