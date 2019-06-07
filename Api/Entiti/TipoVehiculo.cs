using System;
using System.Collections.Generic;

namespace Api.Models
{
    public partial class TipoVehiculo
    {
        public TipoVehiculo()
        {
            ConcentradoTransacciones = new HashSet<ConcentradoTransacciones>();
        }

        public int TipoVehiculoId { get; set; }
        public string ClaveVehiculo { get; set; }

        public ICollection<ConcentradoTransacciones> ConcentradoTransacciones { get; set; }
    }
}
