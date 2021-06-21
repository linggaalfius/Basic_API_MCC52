using API.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<Entity, Repository, Key> : ControllerBase
        where Entity : class
        where Repository : IGeneralRepository<Entity, Key>
    {
        private readonly Repository repository;

        public BaseController (Repository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var get = repository.Get();
            return Ok(new { status = HttpStatusCode.OK, result = get, message = "Success"});
        }

        [HttpGet("{key}")] //beda dari atas, cek nik
        public ActionResult Get(Key key)
        {
            var response = repository.Get(key);
            if (key == null)
            {
                return Ok(new { status = HttpStatusCode.OK, result = response, message = "Tidak ditemukan" });
            }
            else
            {
                return Ok(new { status = HttpStatusCode.OK, result = response, message = "Data ditemukan" });
            }
        }

        [HttpPost]
        public ActionResult Post(Entity e)
        {
            var insert = repository.Insert(e);
            if (insert == 1)
            {
                return Ok("Insert Berhasil");
            }
            else
            {
                return BadRequest("Gagal Insert");
            }
        }

        [HttpDelete]
        public ActionResult Delete(Key key)
        {
            var response = repository.Delete(key);
            if (key == null)
            {
                return BadRequest("Butuh NIK");
            }
            else
            {
                if (response == 1)
                {
                    return Ok("Hapus berhasl");
                }
                else
                {
                    return Ok("Tidak ada");
                }
            }
        }

        [HttpPut]
        public ActionResult Update(Entity e, Key key)
        {
            //var find = repository.Get(key);
            var response = repository.Update(e, key);
            if (key == null)
            {
                var get = NotFound(new { status = HttpStatusCode.NotFound, result = response, message = "Not Found" });
                return get;
            }
            else
            {
                if (response == 1)
                {
                    var get = Ok(new { status = HttpStatusCode.OK, result = response, message = "Success" });
                    return get;
                }
                else
                {
                    var get = NotFound(new { status = HttpStatusCode.NotFound, result = response, message = "Not Success" });
                    return get;
                }
            }
        }
    }
}
