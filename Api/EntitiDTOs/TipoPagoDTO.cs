using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.EntitiDTOs
{
    public class TipoPagoDTO
    {
        public int TipoPagoId { get; set; }
        public string NombrePago { get; set; }

        public List<ConcentradoTransaccionesDTO> ConcentradoTransacciones { get; set; }
        public List<TransaccionesDTO> Transacciones { get; set; }
    }
}
