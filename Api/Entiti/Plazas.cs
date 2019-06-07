using System;
using System.Collections.Generic;

namespace Api.Models
{
    public partial class Plazas
    {
        public Plazas()
        {
            OperadorPlaza = new HashSet<OperadorPlaza>();
            Tramos = new HashSet<Tramos>();
        }

        public string NumeroPlazaCapufe { get; set; }
        public string DelegacionId { get; set; }
        public string NombrePlaza { get; set; }
        public string IpServidor { get; set; }

        public Delegaciones Delegacion { get; set; }
        public ICollection<OperadorPlaza> OperadorPlaza { get; set; }
        public ICollection<Tramos> Tramos { get; set; }
    }
}
