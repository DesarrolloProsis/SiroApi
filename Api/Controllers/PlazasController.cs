using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.EntitiDTOs;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Api.Clases;

namespace Api.Controllers
{
    

    [Route("api/[controller]")]
    [ApiController]
    public class PlazasController : ControllerBase
    {

        Context db = new Context();
        PlazasMetodos PlazaMetodos = new PlazasMetodos();
 
        [HttpGet]
        public JsonResult GetPlazas()
        {
            return new JsonResult(PlazaMetodos.GetPlazasyNumeros());
            //var Plazas = (from c in db.Plazas
            //              select new
            //              {
            //                  NombrePlaza = c.NombrePlaza,
            //                  NumeroPlazaCapufe = c.NumeroPlazaCapufe

            //              }).ToArray();

            //return new JsonResult(Plazas);


        }


        [HttpGet("{param1}/{param2}")]
        public IEnumerable<CarrilesDTO> Get(int param1, string param2)
        {
            var Carriles = db.Carriles;
            return Mapper.Map<IEnumerable<CarrilesDTO>>(Carriles);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }


        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
