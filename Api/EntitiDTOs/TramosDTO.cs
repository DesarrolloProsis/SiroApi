using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.EntitiDTOs
{
    public class TramosDTO
    {
        public int IdGare { get; set; }
        public string NumeroPlazaCapufe { get; set; }
        public string NombreTramo { get; set; }
        public string IpVideo { get; set; }

        public PlazasDTO NumeroPlazaCapufeNavigation { get; set; }
        public List<CarrilesDTO> Carriles { get; set; }
        public List<ConcentradoTransaccionesDTO> ConcentradoTransacciones { get; set; }
        public List<TransaccionesDTO> Transacciones { get; set; }
    }
}
