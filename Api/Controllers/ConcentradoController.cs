using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Api.Clases;
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
        PlazasMetodos PlazaMetodos = new PlazasMetodos();
        TramosMetodos TramoMetodos = new TramosMetodos();
        ConcentradoMetodos ConcentradoMetodos = new ConcentradoMetodos();
        
        


        [Route("Plaza")]
        [HttpGet]
        public JsonResult GetTodasPlazaCruces()
        {

            var Plazas = PlazaMetodos.GetPlazasyNumeros();


            List<PlazaCruce> Lista = new List<PlazaCruce>();

            foreach (var item in Plazas)
            {
                int Cruces = 0;
                var Tramos = TramoMetodos.GetTramos(item.NumeroPlaza);                

                foreach (var item2 in Tramos)
                {
                    Cruces += ConcentradoMetodos.GetPlazaCruces(item2.IdGare);                    

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
            
            var Tramos = TramoMetodos.GetTramosyPlazas(NumeroPlaza);


            List<PlazaCruce> Lista = new List<PlazaCruce>();
            string NombrePlaza = string.Empty;
            int Cruces = 0;


            foreach (var item in Tramos)
            {

                NombrePlaza = item.NombrePlaza;
                Cruces += ConcentradoMetodos.GetPlazaCruces(item.IdGare);                

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

            DateTime FechaInicio = DateTime.ParseExact(fechaInicio, "yyyy-MM-dd", CultureInfo.InvariantCulture).Date;
            DateTime FechaFin = DateTime.ParseExact(fechaFin, "yyyy-MM-dd", CultureInfo.InvariantCulture).Date;

            var Plazas = PlazaMetodos.GetPlazasyNumeros();

            List<TramoCruce> Lista = new List<TramoCruce>();
            string NombrePlaza = string.Empty;
            string NombreTramo = string.Empty;
            int[] Cuerpos = new int[4];


            foreach (var item in Plazas)
            {
                var Tramos = TramoMetodos.GetTramos(item.NumeroPlaza);                

                for (int i = 0; i < Tramos.Count; i++)
                {
                    if (FechaInicio == FechaFin)
                        Cuerpos[i] = ConcentradoMetodos.GetPlazaCruces(Tramos[i].IdGare, FechaFin);                    
                    else
                        Cuerpos[i] = ConcentradoMetodos.GetPlazaCruces(Tramos[i].IdGare, FechaInicio, FechaFin);
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

        [Route("TiposPago/{plaza}/{fechaInicio}/{fechaFin}")]        
        [HttpGet]
        public JsonResult GetCrucesTipoPagoFecha(string Plaza, string fechaInicio, string fechaFin)
        {

            DateTime FechaInicio = DateTime.ParseExact(fechaInicio, "yyyy-MM-dd", CultureInfo.InvariantCulture).Date;
            DateTime FechaFin = DateTime.ParseExact(fechaFin, "yyyy-MM-dd", CultureInfo.InvariantCulture).Date;

            var Tramos = TramoMetodos.GetTramos(Plaza);

            object json = new object();

            List<Transacciones> Transaciones = new List<Transacciones>();

            if (FechaFin == FechaInicio)
            {
                foreach (var item in Tramos)
                {

                    var List = ConcentradoMetodos.GetConcentradoType(item.IdGare, FechaInicio);

                    foreach (var item2 in List)
                    {

                        Transaciones.Add(new Transacciones
                        {
                            Fecha = item2.Fecha,
                            TipoPago = item2.TipoPago,
                            TipoVehiculo = item2.TipoVehiculo,
                            IdTuno = item2.IdTurno

                        });
                    }
                }

                List<CrucesTypePago> ListTipoPago = new List<CrucesTypePago>();
                var TipoPago = db.TipoPago.ToList();

                foreach (var item in TipoPago)
                {
                    var CantidadLinq = Transaciones.Where(x => x.TipoPago == item.TipoPagoId).Count();

                    if (CantidadLinq > 0)
                    {
                        ListTipoPago.Add(new CrucesTypePago
                        {
                            Pago = item.NombrePago,
                            Cantidad = CantidadLinq

                        });
                    }

                }

                List<CrucesTypeVehiculo> ListTipoVehiculo = new List<CrucesTypeVehiculo>();
                var TipoVehiculo = db.TipoVehiculo.ToList();

                foreach (var item in TipoVehiculo)
                {
                    var CantidadLinq = Transaciones.Where(x => x.TipoVehiculo == item.TipoVehiculoId).Count();

                    if (CantidadLinq > 0)
                    {
                        ListTipoVehiculo.Add(new CrucesTypeVehiculo
                        {
                            Vehiculo = item.ClaveVehiculo,
                            Cantidad = CantidadLinq

                        });
                    }
                }

                json = new { ListTipoPago, ListTipoVehiculo };
               
            }

            return new JsonResult(json);

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

            object json = new { Lista, TurnosNombre };


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

        private class Transacciones
        {
            public DateTime Fecha { get; set; }
            public int TipoPago { get; set; }
            public int TipoVehiculo { get; set; }
            public int IdTuno { get; set; }
        }

        private class CrucesTypePago
        {
            public string Pago { get; set; }
            public int Cantidad { get; set; }
        }
        private class CrucesTypeVehiculo
        {
         
            public string Vehiculo { get; set; }
            public int Cantidad { get; set; }
        }


    }
}