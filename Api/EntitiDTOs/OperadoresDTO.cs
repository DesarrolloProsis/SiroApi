using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.EntitiDTOs
{
    public class OperadoresDTO
    {
        public string NumeroCapufeOperador { get; set; }
        public int PuestoId { get; set; }
        public string Nombre { get; set; }
        public string ApellidoMaterno { get; set; }
        public string ApellidoPaterno { get; set; }
        public bool? Activo { get; set; }
        public bool? Sesion { get; set; }
        public string Psd { get; set; }

        public PuestosDTO Puesto { get; set; }
        public List<OperadorPlazaDTO> OperadorPlaza { get; set; }
    }
}
