using System;
using System.Collections.Generic;

namespace Api.Models
{
    public partial class TipoCarril
    {
        public TipoCarril()
        {
            Carriles = new HashSet<Carriles>();
            EficienciasCarriles = new HashSet<EficienciasCarriles>();
        }

        public string TipoCarrilId { get; set; }
        public string Descripcion { get; set; }
        public string Tipo { get; set; }

        public ICollection<Carriles> Carriles { get; set; }
        public ICollection<EficienciasCarriles> EficienciasCarriles { get; set; }
    }
}
