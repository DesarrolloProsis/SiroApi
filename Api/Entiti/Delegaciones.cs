using System;
using System.Collections.Generic;

namespace Api.Models
{
    public partial class Delegaciones
    {
        public Delegaciones()
        {
            Plazas = new HashSet<Plazas>();
        }

        public string DelegacionId { get; set; }
        public string NombreDelegacion { get; set; }

        public ICollection<Plazas> Plazas { get; set; }
    }
}
