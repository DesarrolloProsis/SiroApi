using System;
using System.Collections.Generic;

namespace Api.Models
{
    public partial class TipoPago
    {
        public TipoPago()
        {
            ConcentradoTransacciones = new HashSet<ConcentradoTransacciones>();
            Transacciones = new HashSet<Transacciones>();
        }

        public int TipoPagoId { get; set; }
        public string NombrePago { get; set; }

        public ICollection<ConcentradoTransacciones> ConcentradoTransacciones { get; set; }
        public ICollection<Transacciones> Transacciones { get; set; }
    }
}
