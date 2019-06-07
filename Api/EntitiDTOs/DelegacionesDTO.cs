using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.EntitiDTOs
{
    public class DelegacionesDTO
    {
        public string DelegacionId { get; set; }
        public string NombreDelegacion { get; set; }

        public List<PlazasDTO> Plazas { get; set; }
    }
}
