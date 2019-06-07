using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.EntitiDTOs
{
    public class ConcentradoTransaccionesDTO
    {
        public int NumeroCapufeCarril { get; set; }
        public DateTime Fecha { get; set; }
        public int IdGare { get; set; }
        public int TipoPagoId { get; set; }
        public int TipoVehiculoId { get; set; }
        public int TurnoId { get; set; }
        public int Conteo { get; set; }

        public CarrilesDTO Carriles { get; set; }
        public TramosDTO IdGareNavigation { get; set; }
        public TipoPagoDTO TipoPago { get; set; }
        public TipoVehiculoDTO TipoVehiculo { get; set; }
        public TurnosDTO Turno { get; set; }
    }
}
