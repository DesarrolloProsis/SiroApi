using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TramosController : ControllerBase
    {

        Context db = new Context();

        [HttpGet]
        public JsonResult GetTramos()
        {

            var Tramos = (from c in db.Tramos
                          select new
                          {
                              IdGare = c.IdGare,
                              NumeroCapufe = c.NumeroPlazaCapufe,
                              NombreTramo = c.NombreTramo,

                          }).ToArray();

            return new JsonResult(Tramos);

        }


        [HttpGet("{NumPlaza}")]
        public JsonResult GetTramos(string NumPlaza)
        {

            var TramoEspecifico = (from c in db.Tramos
                                   where c.NumeroPlazaCapufe == NumPlaza
                                   select new
                                   {                          
                                       NumeroCapufe = c.NumeroPlazaCapufe,
                                       NombreTramo = c.NombreTramo,

                                   }).ToArray();

            return new JsonResult(TramoEspecifico);

        }

    }
}