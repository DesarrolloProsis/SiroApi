using System;
using System.Collections.Generic;

namespace Api.Models
{
    public partial class Transacciones
    {
        public int TransaccionId { get; set; }
        public int NumeroEvento { get; set; }
        public int IdGare { get; set; }
        public int NumeroCapufeCarril { get; set; }
        public int TurnoId { get; set; }
        public string LineaCarril { get; set; }
        public int TipoPagoId { get; set; }
        public string TipoPagoDesc { get; set; }
        public string NumeroGea { get; set; }
        public DateTime FechaAperturaTurno { get; set; }
        public string HoraInicioTurno { get; set; }
        public DateTime Fecha { get; set; }
        public string Hora { get; set; }
        public int Folio { get; set; }
        public int IndiceReclasificacion { get; set; }
        public string Pre { get; set; }
        public string Cajero { get; set; }
        public string Post { get; set; }
        public string ObservacionesTt { get; set; }
        public string ObservacionesMp { get; set; }
        public string ObservacionesSecuencia { get; set; }
        public string ObservacionesPaso { get; set; }
        public int NumeroEjes { get; set; }
        public bool Anualado { get; set; }
        public string Comentarios { get; set; }
        public bool Capturado { get; set; }
        public string NumIave { get; set; }

        public Carriles Carriles { get; set; }
        public Tramos IdGareNavigation { get; set; }
        public TipoPago TipoPago { get; set; }
        public Turnos Turno { get; set; }
    }
}
