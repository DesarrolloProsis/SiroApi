using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.EntitiDTOs
{
    public class CarrilesDTO
    {
        public int NumeroCapufeCarril { get; set; }
        public int IdGare { get; set; }
        public string TipoCarrilId { get; set; }
        public int NumeroTramo { get; set; }
        public string LineaCarril { get; set; }
        public string CamaraCarril { get; set; }
        public string CamaraCabina1 { get; set; }
        public string CamaraCabina2 { get; set; }
        public bool EstatusEficiencia { get; set; }
        public DateTime FechaMejorDia { get; set; }

        public TramosDTO IdGareNavigation { get; set; }
        public TipoCarrilDTO TipoCarril { get; set; }
        public List<ConcentradoTransaccionesDTO> ConcentradoTransacciones { get; set; }
        public List<EficienciasCarrilesDTO> EficienciasCarriles { get; set; }
        public List<TransaccionesDTO> Transacciones { get; set; }
    }

    
}
