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

        public string ConvertNombrePlaza(string NombrePlaza)
        {
            var Plazas = (from c in db.Plazas
                          where c.NombrePlaza == NombrePlaza
                          select new PlazayNumero
                          {
                              NumeroPlaza = c.NumeroPlazaCapufe,

                          }).ToList();
            if (Plazas.Count > 0) return Plazas[0].NumeroPlaza;
            else return "error conversion";
            
        }



    }

    class PlazayNumero
    {
        public string  NombrePlaza { get; set; }
        public string  NumeroPlaza { get; set; }
    }


}
