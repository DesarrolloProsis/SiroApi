using System;
using System.Collections.Generic;

namespace Api.Models
{
    public partial class Carriles
    {
        public Carriles()
        {
            ConcentradoTransacciones = new HashSet<ConcentradoTransacciones>();
            EficienciasCarriles = new HashSet<EficienciasCarriles>();
            Transacciones = new HashSet<Transacciones>();
        }

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

        public Tramos IdGareNavigation { get; set; }
        public TipoCarril TipoCarril { get; set; }
        public ICollection<ConcentradoTransacciones> ConcentradoTransacciones { get; set; }
        public ICollection<EficienciasCarriles> EficienciasCarriles { get; set; }
        public ICollection<Transacciones> Transacciones { get; set; }
    }
}
