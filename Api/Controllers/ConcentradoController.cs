using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Api.Controllers
{

    [Route("api/[controller]")]

    [ApiController]
    public class ConcentradoController : ControllerBase
    {
        Context db = new Context();


        [Route("Plaza")]
        [HttpGet]
        public JsonResult GetPlazaCruces()
        {

            var Plazas = (from c in db.Plazas
                          select new
                          {
                              NumeroPlaza = c.NumeroPlazaCapufe,
                              NombrePlaza = c.NombrePlaza,

                          }).ToList();



            List<PlazaCruce> Lista = new List<PlazaCruce>();

            foreach (var item in Plazas)
            {
                int Cruces = 0;
                var Tramos = db.Tramos.Where(x => x.NumeroPlazaCapufe == item.NumeroPlaza).ToList();

                foreach (var item2 in Tramos)
                {

                    Cruces += db.ConcentradoTransacciones.Where(x => x.IdGare == item2.IdGare).Count();

                }

                Lista.Add(new PlazaCruce
                {
                    NombrePlaza = item.NombrePlaza,
                    Cruces = Cruces
                });

            }

            return new JsonResult(Lista);
        }

        [Route("Plaza/{NumeroPlaza}")]
        [HttpGet]
        public JsonResult GetPlazaCruces(string NumeroPlaza)
        {

            var Tramos = (from c in db.Plazas
                          join p in db.Tramos on c.NumeroPlazaCapufe equals p.NumeroPlazaCapufe
                          where c.NumeroPlazaCapufe == NumeroPlaza
                          select new
                          {

                              NombrePlaza = c.NombrePlaza,
                              IdGare = p.IdGare,
                          }
                        ).ToList();


            List<PlazaCruce> Lista = new List<PlazaCruce>();
            string NombrePlaza = string.Empty;
            int Cruces = 0;


            foreach (var item in Tramos)
            {

                NombrePlaza = item.NombrePlaza;
                Cruces += db.ConcentradoTransacciones.Where(x => x.IdGare == item.IdGare).Count();

            }

            Lista.Add(new PlazaCruce
            {
                NombrePlaza = NombrePlaza,
                Cruces = Cruces
            });

            return new JsonResult(Lista);
        }



        [Route("Plaza/Tramo")]
        [Route("Plaza/Tramo/{fechaInicio}/{fechaFin}")]
        [HttpGet]
        public JsonResult GetPlazaTramoCrucesFecha(string fechaInicio, string fechaFin)
        {
            var dsgsdfg = fechaInicio;
            DateTime FechaInicio = DateTime.ParseExact(fechaInicio, "yyyy-MM-dd", CultureInfo.InvariantCulture).Date;
            DateTime FechaFin = DateTime.ParseExact(fechaFin, "yyyy-MM-dd", CultureInfo.InvariantCulture).Date;

            var Plazas = (from c in db.Plazas
                          select new
                          {

                              NombrePlaza = c.NombrePlaza,
                              NumeroCapufe = c.NumeroPlazaCapufe,
                          }
                      ).ToList();


            List<TramoCruce> Lista = new List<TramoCruce>();
            string NombrePlaza = string.Empty;
            string NombreTramo = string.Empty;
            int[] Cuerpos = new int[4];



            foreach (var item in Plazas)
            {

                var Tramos = db.Tramos.Where(x => x.NumeroPlazaCapufe == item.NumeroCapufe).ToList();

                for (int i = 0; i < Tramos.Count; i++)
                {
                    if(FechaInicio == FechaFin)
                        Cuerpos[i] = db.ConcentradoTransacciones.Where(x => x.IdGare == Tramos[i].IdGare && x.Fecha == FechaInicio).Count();
                    else
                        Cuerpos[i] = db.ConcentradoTransacciones.Where(x => x.IdGare == Tramos[i].IdGare && x.Fecha >= FechaInicio && x.Fecha <= FechaFin).Count();

                }

                int CuerpoA = Cuerpos[0];
                int CuerpoB = Cuerpos[1];
                int CuerpoC = Cuerpos[2];
                int CuerpoD = Cuerpos[3];

                Lista.Add(new TramoCruce
                {
                    NombrePlaza = item.NombrePlaza,
                    CuerpoA = CuerpoA,
                    CuerpoB = CuerpoB,
                    CuerpoC = CuerpoC,
                    CuerpoD = CuerpoD

                });

            }


            return new JsonResult(Lista);
        }


        [Route("Plaza/Turnos")]
        [HttpGet]
        public JsonResult GetPlazaTurnosCruces()
        {

            var Plazas = (from c in db.Plazas
                          select new
                          {

                              NombrePlaza = c.NombrePlaza,
                              NumeroCapufe = c.NumeroPlazaCapufe,
                          }
                      ).ToList();

            var Turnos = db.Turnos.Where(x => x.TurnoId != 0).ToList();


            List<PlazaTurnoCruce> Lista = new List<PlazaTurnoCruce>();
            string NombrePlaza = string.Empty;
            string NombreTramo = string.Empty;
            string[] TurnosNombre = new[] { "nombrePlaza", "turnoMatutino", "turnoVespertino", "turnoNocturno" };
            int[] CrucesTurno = new int[4];




            foreach (var item in Plazas)
            {
                for (int i = 1; i <= 3; i++)
                {
                    var Tramos = db.Tramos.Where(x => x.NumeroPlazaCapufe == item.NumeroCapufe).ToList();
                    int AcumuladorCruces = 0;

                    for (int e = 0; e < Tramos.Count; e++)
                    {
                        AcumuladorCruces += db.ConcentradoTransacciones.Where(x => x.IdGare == Tramos[e].IdGare && x.TurnoId == i).Count();

                    }

                    CrucesTurno[i] = AcumuladorCruces;

                }

                Lista.Add(new PlazaTurnoCruce
                {
                    NombrePlaza = item.NombrePlaza,
                    TurnoMatutino = CrucesTurno[1],
                    TurnoVespertino = CrucesTurno[2],
                    TurnoNocturno = CrucesTurno[3]

                });


            }
     
            object json = new { Lista, TurnosNombre};


            return new JsonResult(json);
        }
        private class PlazaCruce
        {
            public string NombrePlaza { get; set; }
            public int Cruces { get; set; }


        }
        private class TramoCruce
        {
            public string NombrePlaza { get; set; }
            public int CuerpoA { get; set; }
            public int CuerpoB { get; set; }
            public int CuerpoC { get; set; }
            public int CuerpoD { get; set; }

        }

        private class PlazaTurnoCruce
        {
            public string NombrePlaza { get; set; }
            public int TurnoMatutino { get; set; }
            public int TurnoVespertino { get; set; }
            public int TurnoNocturno { get; set; }

        }

    }
}