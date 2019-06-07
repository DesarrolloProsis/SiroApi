using System;
using System.Collections.Generic;

namespace Api.Models
{
    public partial class Turnos
    {
        public Turnos()
        {
            ConcentradoTransacciones = new HashSet<ConcentradoTransacciones>();
            Transacciones = new HashSet<Transacciones>();
        }

        public int TurnoId { get; set; }
        public string NombreTurno { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFin { get; set; }

        public ICollection<ConcentradoTransacciones> ConcentradoTransacciones { get; set; }
        public ICollection<Transacciones> Transacciones { get; set; }
    }
}
