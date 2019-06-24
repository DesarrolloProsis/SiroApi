using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Clases
{
    class PlazasMetodos 
    {
        Context db = new Context();

        ///<summary>
        /// Obtine una lista de con todos las Plazas
        /// </summary>
        public List<PlazayNumero> GetPlazasyNumeros()
        {
            var Plazas = (from c in db.Plazas
                          select new PlazayNumero
                          {                        
                              NombrePlaza = c.NombrePlaza,
                              NumeroPlaza = c.NumeroPlazaCapufe,

                          }).ToList();

            return Plazas;
        }


    }

    class PlazayNumero
    {
        public string  NombrePlaza { get; set; }
        public string  NumeroPlaza { get; set; }
    }


}
