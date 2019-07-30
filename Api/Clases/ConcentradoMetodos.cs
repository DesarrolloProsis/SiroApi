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
        TramosMetodos TramosMetodos = new TramosMetodos();

        public List<Concentrados> GetCruces(string NumeroPlaza, DateTime FechaInicio, DateTime FechaFin)
        {
                
                var Lista = (from c in db.ConcentradoTransacciones 
                             join p in db.Tramos on c.IdGare equals p.IdGare
                             where c.Fecha >= FechaInicio && c.Fecha < FechaInicio.AddDays(1)
                             where p.NumeroPlazaCapufe == NumeroPlaza
                                select new Concentrados
                                {
                                 Fecha = c.Fecha,
                                 IdGare = c.IdGare,
                                 IdTurno = c.TurnoId,
                                 TipoPago = c.TipoPagoId,
                                 TipoVehiculo = c.TipoVehiculoId,
                                 NumCarril = c.NumeroCapufeCarril

                             }).ToList();                                   

            return Lista;
        }

        public List<Concentrados> GetCruces(string NumeroPlaza, DateTime FechaInicio)
        {
            var Lista = (from c in db.ConcentradoTransacciones
                         join p in db.Tramos on c.IdGare equals p.IdGare
                         where c.Fecha >= FechaInicio && c.Fecha < FechaInicio.AddDays(1)
                         where p.NumeroPlazaCapufe == NumeroPlaza
                         select new Concentrados
                         {
                             Fecha = c.Fecha,
                             IdGare = c.IdGare,
                             IdTurno = c.TurnoId,
                             TipoPago = c.TipoPagoId,
                             TipoVehiculo = c.TipoVehiculoId,
                             NumCarril = c.NumeroCapufeCarril

                         }).ToList();

            return Lista;
        }

        public int GetCrucesCount(string NumeroPlaza, DateTime FechaInicio, DateTime FechaFin)
        {
            var Lista = (from c in db.ConcentradoTransacciones
                         join p in db.Tramos on c.IdGare equals p.IdGare
                         where c.Fecha >= FechaInicio && c.Fecha < FechaInicio.AddDays(1)
                         where p.NumeroPlazaCapufe == NumeroPlaza
                         select new Concentrados
                         {
                             Fecha = c.Fecha,
                             IdGare = c.IdGare,
                             IdTurno = c.TurnoId,
                             TipoPago = c.TipoPagoId,
                             TipoVehiculo = c.TipoVehiculoId,
                             NumCarril = c.NumeroCapufeCarril

                         }).Count();

            return Lista;
        }
  
        public int GetCrucesCount(string NumeroPlaza, DateTime FechaInicio)
        {
            var Lista = (from c in db.ConcentradoTransacciones
                         join p in db.Tramos on c.IdGare equals p.IdGare
                         where c.Fecha >= FechaInicio && c.Fecha < FechaInicio.AddDays(1)
                         where p.NumeroPlazaCapufe == NumeroPlaza
                         select new Concentrados
                         {
                             Fecha = c.Fecha,
                             IdGare = c.IdGare,
                             IdTurno = c.TurnoId,
                             TipoPago = c.TipoPagoId,
                             TipoVehiculo = c.TipoVehiculoId,
                             NumCarril = c.NumeroCapufeCarril

                         }).Count();

            return Lista;
        }

        public int GetCrucesTramo(int idgare, DateTime FechaInicio, DateTime FechaFin)
        {
            var Lista = (from c in db.ConcentradoTransacciones
                         where c.Fecha >= FechaInicio && c.Fecha < FechaFin
                         where c.IdGare == idgare
                         select new Concentrados
                         {
                             Fecha = c.Fecha,
                             IdGare = c.IdGare,
                             IdTurno = c.TurnoId,
                             TipoPago = c.TipoPagoId,
                             TipoVehiculo = c.TipoVehiculoId,
                             NumCarril = c.NumeroCapufeCarril

                         }).Count();

            return Lista;

        }

        public int GetCrucesTramo(int idgare, DateTime FechaInicio)
        {
            var Lista = (from c in db.ConcentradoTransacciones                       
                         where c.Fecha >= FechaInicio && c.Fecha < FechaInicio.AddDays(1)
                         where c.IdGare == idgare
                         select new Concentrados
                         {
                             Fecha = c.Fecha,
                             IdGare = c.IdGare,
                             IdTurno = c.TurnoId,
                             TipoPago = c.TipoPagoId,
                             TipoVehiculo = c.TipoVehiculoId,
                             NumCarril = c.NumeroCapufeCarril

                         }).Count();

            return Lista;

        }

  

        public List<Concentrados> GetCrucesTurnos(string NumeroPlaza , int idTurno, DateTime FechaInicio, DateTime FechaFin)
        {
            var Lista = (from c in db.ConcentradoTransacciones
                         join p in db.Tramos on c.IdGare equals p.IdGare
                         where c.Fecha >= FechaInicio && c.Fecha < FechaInicio.AddDays(1)
                         where p.NumeroPlazaCapufe == NumeroPlaza
                         where c.TurnoId == idTurno
                         select new Concentrados
                         {
                             Fecha = c.Fecha,
                             IdGare = c.IdGare,
                             IdTurno = c.TurnoId,
                             TipoPago = c.TipoPagoId,
                             TipoVehiculo = c.TipoVehiculoId,
                             NumCarril = c.NumeroCapufeCarril

                         }).ToList();

            return Lista;
        }
        public int GetCrucesTurnosCount(string NumeroPlaza, int idTurno, DateTime FechaInicio, DateTime FechaFin)
        {
            var Lista = (from c in db.ConcentradoTransacciones
                         join p in db.Tramos on c.IdGare equals p.IdGare
                         where c.Fecha >= FechaInicio && c.Fecha < FechaFin
                         where p.NumeroPlazaCapufe == NumeroPlaza
                         where c.TurnoId == idTurno
                         select new Concentrados
                         {
                             Fecha = c.Fecha,
                             IdGare = c.IdGare,
                             IdTurno = c.TurnoId,
                             TipoPago = c.TipoPagoId,
                             TipoVehiculo = c.TipoVehiculoId,
                             NumCarril = c.NumeroCapufeCarril

                         }).Count();

            return Lista;
        }
        public int GetCrucesTurnosCount(string NumeroPlaza, int idTurno, DateTime FechaInicio)
        {
            var Lista = (from c in db.ConcentradoTransacciones
                         join p in db.Tramos on c.IdGare equals p.IdGare
                         where c.Fecha >= FechaInicio && c.Fecha < FechaInicio.AddDays(1)
                         where p.NumeroPlazaCapufe == NumeroPlaza
                         where c.TurnoId == idTurno
                         select new Concentrados
                         {
                             Fecha = c.Fecha,
                             IdGare = c.IdGare,
                             IdTurno = c.TurnoId,
                             TipoPago = c.TipoPagoId,
                             TipoVehiculo = c.TipoVehiculoId,
                             NumCarril = c.NumeroCapufeCarril

                         }).Count();

            return Lista;
        }

        public int  GetCrucesTypePagoCount(string numeroPlaza, int idPago, DateTime FechaInicio, DateTime FechaFin)
        {
            var Transacciones = GetCruces(numeroPlaza, FechaInicio, FechaFin);
            var Total = Transacciones.Where(x => x.TipoPago == idPago).Count();
            return Total;
        }
        public int GetCrucesTypePagoCount(string numeroPlaza, int idPago, DateTime FechaInicio)
        {
            var Transacciones = GetCruces(numeroPlaza, FechaInicio);
            var Total = Transacciones.Where(x => x.TipoPago == idPago).Count();
            return Total;
        }

        public int GetCrucesTypeVehiculoCount(string numeroPlaza, int idVehiculo, DateTime FechaInicio, DateTime FechaFin)
        {
            var Transacciones = GetCruces(numeroPlaza, FechaInicio, FechaFin);
            var Total = Transacciones.Where(x => x.TipoVehiculo == idVehiculo).Count();
            return Total;
        }
        public int GetCrucesTypeVehiculoCount(string numeroPlaza, int idVehiculo, DateTime FechaInicio)
        {
            var Transacciones = GetCruces(numeroPlaza, FechaInicio);
            var Total = Transacciones.Where(x => x.TipoVehiculo == idVehiculo).Count();
            return Total;
        }



    }
    class Concentrados
    {
        public DateTime Fecha { get; set; }
        public int TipoPago { get; set; }
        public int TipoVehiculo { get; set; }
        public int NumCarril { get; set; }
        public int IdTurno { get; set; }
        public int IdGare { get; set; }
    }
}
