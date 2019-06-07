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
    public class TurnosController : ControllerBase
    {

        Context db = new Context();

        [HttpGet]
        public JsonResult GetTurnos()
        {

            var Turnos = (from c in db.Turnos
                          where c.TurnoId != 0
                          select new
                          {
                              TurnoId = c.TurnoId,
                              Hora_Inicio = c.HoraInicio,
                              Hora_Fin = c.HoraFin,

                          }).ToArray();

            return new JsonResult(Turnos);

        }
    }
}