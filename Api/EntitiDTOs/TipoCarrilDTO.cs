using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.EntitiDTOs
{
    public class TipoCarrilDTO
    {

        public string TipoCarrilId { get; set; }
        public string Descripcion { get; set; }
        public string Tipo { get; set; }

        public List<CarrilesDTO> Carriles { get; set; }
        public List<EficienciasCarrilesDTO> EficienciasCarriles { get; set; }
    }
}
