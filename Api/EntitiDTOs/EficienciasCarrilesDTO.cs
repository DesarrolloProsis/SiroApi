using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.EntitiDTOs
{
    public class EficienciasCarrilesDTO
    {
        public int NumeroCapufeCarril { get; set; }
        public int IdGare { get; set; }
        public string TipoCarrilId { get; set; }
        public DateTime Fecha { get; set; }
        public float EficienciaMatutinoPre { get; set; }
        public float EficienciaVespertinoPre { get; set; }
        public float EficienciaNocturnoPre { get; set; }
        public float EficienciaMatutinoPost { get; set; }
        public float EficienciaVespertinoPost { get; set; }
        public float EficienciaNocturnoPost { get; set; }
        public float EficienciaDiaPre { get; set; }
        public float EficienciaDiaPost { get; set; }

        public CarrilesDTO Carriles { get; set; }
        public TipoCarrilDTO TipoCarril { get; set; }
    }
}
