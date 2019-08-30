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

        PlazasMetodos PlazaMetodos = new PlazasMetodos();
        TramosMetodos TramoMetodos = new TramosMetodos();
        ConcentradoMetodos ConcentradoMetodos = new ConcentradoMetodos();
        GraficasMetodos GraficasMetodos = new GraficasMetodos();



        [Route("Graficas/Full/{fechaInicio}/{fechaFin}")]
        [HttpGet]
        public JsonResult GetGraficasFull(string fechaInicio, string fechaFin)
        {

            DateTime FechaInicio = DateTime.ParseExact(fechaInicio, "yyyy-MM-dd", CultureInfo.InvariantCulture).Date;
            DateTime FechaFin = DateTime.ParseExact(fechaFin, "yyyy-MM-dd", CultureInfo.InvariantCulture).Date;

            var Lista = GraficasMetodos.GraficaFull(FechaInicio, FechaFin);

            return new JsonResult(Lista);
        }

        [Route("Graficas/Tramos/{fechaInicio}/{fechaFin}")]
        [HttpGet]
        public JsonResult GetGraficasTramosFull(string fechaInicio, string fechaFin)
        {

            DateTime FechaInicio = DateTime.ParseExact(fechaInicio, "yyyy-MM-dd", CultureInfo.InvariantCulture).Date;
            DateTime FechaFin = DateTime.ParseExact(fechaFin, "yyyy-MM-dd", CultureInfo.InvariantCulture).Date;

            var Lista = GraficasMetodos.GraficaTramosFull(FechaInicio, FechaFin);

            return new JsonResult(Lista);
        }

        [Route("Graficas/Tramos/{plaza}/{fechaInicio}/{fechaFin}")]
        [HttpGet]
        public JsonResult GetGraficasTramos(string plaza, string fechaInicio, string fechaFin)
        {

            DateTime FechaInicio = DateTime.ParseExact(fechaInicio, "yyyy-MM-dd", CultureInfo.InvariantCulture).Date;
            DateTime FechaFin = DateTime.ParseExact(fechaFin, "yyyy-MM-dd", CultureInfo.InvariantCulture).Date;

            var Lista = GraficasMetodos.GraficaTramos(plaza, FechaInicio, FechaFin);

            return new JsonResult(Lista);
        }

        [Route("Graficas/Tramos/TypePago/{plaza}/{fechaInicio}/{fechaFin}")]
        [HttpGet]
        public JsonResult GetGraficasTramoTypePago(string plaza, string fechaInicio, string fechaFin)
        {
            DateTime FechaInicio = DateTime.ParseExact(fechaInicio, "yyyy-MM-dd", CultureInfo.InvariantCulture).Date;
            DateTime FechaFin = DateTime.ParseExact(fechaFin, "yyyy-MM-dd", CultureInfo.InvariantCulture).Date;

            var Lista = GraficasMetodos.GraficaTramoTypePago(plaza, FechaInicio, FechaFin);

            return new JsonResult(Lista);

        }
        [Route("Graficas/Tramos/TypeVehiculo/{plaza}/{fechaInicio}/{fechaFin}")]
        [HttpGet]
        public JsonResult GetGraficasTramoTypeVehiculo(string plaza, string fechaInicio, string fechaFin)
        {
            DateTime FechaInicio = DateTime.ParseExact(fechaInicio, "yyyy-MM-dd", CultureInfo.InvariantCulture).Date;
            DateTime FechaFin = DateTime.ParseExact(fechaFin, "yyyy-MM-dd", CultureInfo.InvariantCulture).Date;

            var Lista = GraficasMetodos.GraficaTramoTypeVehiculo(plaza, FechaInicio, FechaFin);

            return new JsonResult(Lista);

        }

        [Route("Graficas/Turnos/TypePago/{plaza}/{fechaInicio}/{fechaFin}")]
        [HttpGet]
        public JsonResult GetGraficasTurnoTypePago(string plaza, string fechaInicio, string fechaFin)
        {
            DateTime FechaInicio = DateTime.ParseExact(fechaInicio, "yyyy-MM-dd", CultureInfo.InvariantCulture).Date;
            DateTime FechaFin = DateTime.ParseExact(fechaFin, "yyyy-MM-dd", CultureInfo.InvariantCulture).Date;

            var Lista = GraficasMetodos.GraficasTurnosTypePago(plaza, FechaInicio, FechaFin);

            return new JsonResult(Lista);
        }

        [Route("Graficas/Turnos/TypeVehiculo/{plaza}/{fechaInicio}/{fechaFin}")]
        [HttpGet]
        public JsonResult GetGraficasTurnoTypeVehiculo(string plaza, string fechaInicio, string fechaFin)
        {
            DateTime FechaInicio = DateTime.ParseExact(fechaInicio, "yyyy-MM-dd", CultureInfo.InvariantCulture).Date;
            DateTime FechaFin = DateTime.ParseExact(fechaFin, "yyyy-MM-dd", CultureInfo.InvariantCulture).Date;

            var Lista = GraficasMetodos.GraficasTurnosTypeVehiculo(plaza, FechaInicio, FechaFin);

            return new JsonResult(Lista);
        }


        [Route("Graficas/Turnos/{fechaInicio}/{fechaFin}")]
        [HttpGet]
        public JsonResult GetGraficasTurnosFull(string fechaInicio, string fechaFin)
        {

            DateTime FechaInicio = DateTime.ParseExact(fechaInicio, "yyyy-MM-dd", CultureInfo.InvariantCulture).Date;
            DateTime FechaFin = DateTime.ParseExact(fechaFin, "yyyy-MM-dd", CultureInfo.InvariantCulture).Date;

            var Lista = GraficasMetodos.GraficaTurnosFull(FechaInicio, FechaFin);

            return new JsonResult(Lista);
        }

        [Route("Graficas/Turnos/{plaza}/{fechaInicio}/{fechaFin}")]
        [HttpGet]
        public JsonResult GetGraficasTurnosFull(string plaza ,string fechaInicio, string fechaFin)
        {

            DateTime FechaInicio = DateTime.ParseExact(fechaInicio, "yyyy-MM-dd", CultureInfo.InvariantCulture).Date;
            DateTime FechaFin = DateTime.ParseExact(fechaFin, "yyyy-MM-dd", CultureInfo.InvariantCulture).Date;

            var Lista = GraficasMetodos.GraficaTurnos(plaza, FechaInicio, FechaFin);

            return new JsonResult(Lista);
        }

        [Route("Graficas/TypePago/{plaza}/{fechaInicio}/{fechaFin}")]
        [HttpGet]
        public JsonResult GePlazaTypePago(string plaza, string fechaInicio, string fechaFin)
        {
            DateTime FechaInicio = DateTime.ParseExact(fechaInicio, "yyyy-MM-dd", CultureInfo.InvariantCulture).Date;
            DateTime FechaFin = DateTime.ParseExact(fechaFin, "yyyy-MM-dd", CultureInfo.InvariantCulture).Date;

            var Lista = GraficasMetodos.GraficaTramoTypePagoFull(plaza,FechaInicio, FechaFin);

            return new JsonResult(Lista);

        }

        [Route("Graficas/TypeVehiculo/{plaza}/{fechaInicio}/{fechaFin}")]
        [HttpGet]
        public JsonResult GePlazaTypeVehiculo(string plaza, string fechaInicio, string fechaFin)
        {
            DateTime FechaInicio = DateTime.ParseExact(fechaInicio, "yyyy-MM-dd", CultureInfo.InvariantCulture).Date;
            DateTime FechaFin = DateTime.ParseExact(fechaFin, "yyyy-MM-dd", CultureInfo.InvariantCulture).Date;

            var Lista = GraficasMetodos.GraficaTramoTypeVehiculoFull(plaza, FechaInicio, FechaFin);

            return new JsonResult(Lista);

        }


    }
}