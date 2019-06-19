using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Clases
{
    class ConcentradoMetodos
    {
        Context db = new Context();
        public int GetPlazaCruces(int idgare)
        {
            return db.ConcentradoTransacciones.Where(x => x.IdGare == idgare).Count();
        }
        public int GetPlazaCruces(int idgare, DateTime fechaInicio)
        {
            return db.ConcentradoTransacciones.Where(x => x.IdGare == idgare && x.Fecha == fechaInicio).Count(); ;
        }
        public int GetPlazaCruces(int idgare, DateTime fechaInicio, DateTime fechaFin)
        {
            return db.ConcentradoTransacciones.Where(x => x.IdGare == idgare && x.Fecha >= fechaInicio && x.Fecha <= fechaFin).Count(); ;
        }
        public List<ConcentradoType> GetConcentradoType(int idgare, DateTime fechaInicio)
        {
            var Concentrado = (from t in db.ConcentradoTransacciones
                               where t.Fecha == fechaInicio
                               where t.IdGare == idgare
                               select new ConcentradoType
                               {
                                   Fecha = t.Fecha,
                                   TipoPago = t.TipoPagoId,
                                   TipoVehiculo = t.TipoVehiculoId,
                                   IdTurno = t.TurnoId

                               }).ToList();

            return Concentrado;
        }

    }
    class ConcentradoType
    {
        public DateTime Fecha { get; set; }
        public int TipoPago { get; set; }
        public int TipoVehiculo { get; set; }
        public int IdTurno { get; set; }
    }
}
