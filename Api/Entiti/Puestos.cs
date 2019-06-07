using System;
using System.Collections.Generic;

namespace Api.Models
{
    public partial class Puestos
    {
        public Puestos()
        {
            Operadores = new HashSet<Operadores>();
        }

        public int PuestoId { get; set; }
        public string NombrePuesto { get; set; }

        public ICollection<Operadores> Operadores { get; set; }
    }
}
