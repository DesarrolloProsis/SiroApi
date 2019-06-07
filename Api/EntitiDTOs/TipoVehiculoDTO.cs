using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.EntitiDTOs
{
    public class TipoVehiculoDTO
    {

        public int TipoVehiculoId { get; set; }
        public string ClaveVehiculo { get; set; }

        public List<ConcentradoTransaccionesDTO> ConcentradoTransacciones { get; set; }
    }
}
