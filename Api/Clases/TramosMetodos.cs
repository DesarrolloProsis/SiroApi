using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Clases
{
    class TramosMetodos
    {
        Context db = new Context();
        public List<TramosSinIp> GetTramosSinIp()
        {
            var Tramos = (from t in db.Tramos
                          select new TramosSinIp
                          {
                              IdGare = t.IdGare,
                              NumeroPlaza = t.NumeroPlazaCapufe,
                              NombreTramo = t.NombreTramo

                          }).ToList();

            return Tramos;
        }
        public List<TramosFull> GetTramos()
        {
            var Tramos = (from t in db.Tramos
                          select new TramosFull
                          {
                              IdGare = t.IdGare,
                              NumeroPlaza = t.NumeroPlazaCapufe,
                              NombreTramo = t.NombreTramo,
                              IpVideo = t.IpVideo

                          }).ToList();

            return Tramos;
        }
        ///<summary>
        /// Obtine una lista de tramos por su NumeroCapufe o NombrePlaza
        /// </summary>
        public List<TramosFull> GetTramos(string nombreOnumero)
        {
            if (nombreOnumero.Length > 5)
            {
                var Tramos = (from t in db.Tramos
                              where t.NumeroPlazaCapufe == nombreOnumero
                              select new TramosFull
                              {
                                  IdGare = t.IdGare,
                                  NumeroPlaza = t.NumeroPlazaCapufe,
                                  NombreTramo = t.NombreTramo,
                                  IpVideo = t.IpVideo

                              }).ToList();

                return Tramos;
            }
            else
            {
                var Tramos = (from t in db.Tramos
                              join p in db.Plazas on t.NumeroPlazaCapufe equals p.NumeroPlazaCapufe
                              where p.NombrePlaza == nombreOnumero
                              select new TramosFull
                              {
                                  IdGare = t.IdGare,
                                  NumeroPlaza = t.NumeroPlazaCapufe,
                                  NombreTramo = t.NombreTramo,
                                  IpVideo = t.IpVideo

                              }).ToList();

                return Tramos;
            }
        }
        public List<TramosPlazas> GetTramosyPlazas(string numeroplaza)
        {
            var Tramos = (from c in db.Plazas
                          join p in db.Tramos on c.NumeroPlazaCapufe equals p.NumeroPlazaCapufe
                          where c.NumeroPlazaCapufe == numeroplaza
                          select new TramosPlazas
                          {
                              NombrePlaza = c.NombrePlaza,
                              IdGare = p.IdGare,
                          }
                        ).ToList();

            return Tramos;
        }


    }
    class TramosSinIp
    {
        public int IdGare { get; set; }
        public string NumeroPlaza { get; set; }
        public string NombreTramo { get; set; }
    }
    class TramosFull
    {
        public int IdGare { get; set; }
        public string NumeroPlaza { get; set; }
        public string NombreTramo { get; set; }
        public string IpVideo { get; set; }
    }

    class TramosPlazas
    { 
      public string NombrePlaza { get; set; }
      public int IdGare { get; set; }

    }


}


