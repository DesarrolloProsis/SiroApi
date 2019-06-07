using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.EntitiDTOs
{
    public class PuestosDTO
    {
        public int PuestoId { get; set; }
        public string NombrePuesto { get; set; }

        public List<OperadoresDTO> Operadores { get; set; }
    }
}
