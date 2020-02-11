using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Clases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EficienciasController : ControllerBase
    {
        Carriles carriles = new Carriles();
        Eficiencias eficiencias = new Eficiencias();

        [Route("Carriles/{numeroPlaza}")]
        [HttpGet]
        public JsonResult GetGraficasFull(string numeroPlaza)
        {                                 
            return new JsonResult(carriles.CarrilesPlaza(numeroPlaza));
        }

        [Route("DiscrepanciasDia/{numeroPlaza}/{fechaInicio}/{fechaFin}/{Carril}")]
        [HttpGet]
        public JsonResult GetGraficasFull(string numeroPlaza, string fechaInicio, string fechaFin, string carril)
        {
            if (fechaInicio == fechaFin){

                var Json = eficiencias.DiscrepanciasDia(numeroPlaza, fechaInicio, carril);
                return new JsonResult(Json);
            }
            else
            {

                var Json = eficiencias.DiscrepanciasDia(numeroPlaza, fechaInicio, fechaFin, carril);
                return new JsonResult(Json);
            }
        }

        [Route("GraficaDia/{plaza}/{fecha}/{carril}")]
        [HttpGet]
        public JsonResult GetGraficasFull(string plaza, string fecha, string carril)
        {
            var graficaDia = eficiencias.GraficaDia(plaza, fecha, carril);
            var graficaMatutino = eficiencias.GraficaMatutino(plaza, fecha, carril);
            var graficaVespertino = eficiencias.GraficaVespertino(plaza, fecha, carril);
            var graficaNocturno = eficiencias.GraficaNocturno(plaza, fecha, carril);
            var tablaPrePost = eficiencias.TablaPrePost(plaza, fecha, carril);

            object Json = new { graficaDia, graficaMatutino, graficaVespertino, graficaNocturno, tablaPrePost };

            return new JsonResult(Json);
        }


        [Route("Pruebas")]
        [HttpPost]
        public string CarrilesPlaza([FromBody] pot value)
        {

            try 
            {

                var ok = value;

                
                return "SI entor y mando datos xd";
            }
            catch (Exception ex)
            {
                return null;
            }
        }



    }

    public class pot
    {
        public string CapufeLaneNum { get; set; }
        public string Lane { get; set; }
        public int LaneType { get; set; }
        public string SquaresCatalogId { get; set; }
        public object DTCTechnical { get; set; }

    }

}
