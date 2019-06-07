using System;
using System.Collections.Generic;

namespace Api.Models
{
    public partial class ConcentradoTransacciones
    {
        public int NumeroCapufeCarril { get; set; }
        public DateTime Fecha { get; set; }
        public int IdGare { get; set; }
        public int TipoPagoId { get; set; }
        public int TipoVehiculoId { get; set; }
        public int TurnoId { get; set; }
        public int Conteo { get; set; }

        public Carriles Carriles { get; set; }
        public Tramos IdGareNavigation { get; set; }
        public TipoPago TipoPago { get; set; }
        public TipoVehiculo TipoVehiculo { get; set; }
        public Turnos Turno { get; set; }
    }
}
