using System;
using System.Collections.Generic;

namespace Api.Models
{
    public partial class Tramos
    {
        public Tramos()
        {
            Carriles = new HashSet<Carriles>();
            ConcentradoTransacciones = new HashSet<ConcentradoTransacciones>();
            Transacciones = new HashSet<Transacciones>();
        }

        public int IdGare { get; set; }
        public string NumeroPlazaCapufe { get; set; }
        public string NombreTramo { get; set; }
        public string IpVideo { get; set; }

        public Plazas NumeroPlazaCapufeNavigation { get; set; }
        public ICollection<Carriles> Carriles { get; set; }
        public ICollection<ConcentradoTransacciones> ConcentradoTransacciones { get; set; }
        public ICollection<Transacciones> Transacciones { get; set; }
    }
}
