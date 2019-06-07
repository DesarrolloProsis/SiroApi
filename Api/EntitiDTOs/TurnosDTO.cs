using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.EntitiDTOs
{
    public class TurnosDTO
    {
        public int TurnoId { get; set; }
        public string NombreTurno { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFin { get; set; }

        public List<ConcentradoTransaccionesDTO> ConcentradoTransacciones { get; set; }
        public List<TransaccionesDTO> Transacciones { get; set; }
    }
}
