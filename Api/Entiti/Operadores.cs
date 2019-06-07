using System;
using System.Collections.Generic;

namespace Api.Models
{
    public partial class Operadores
    {
        public Operadores()
        {
            OperadorPlaza = new HashSet<OperadorPlaza>();
        }

        public string NumeroCapufeOperador { get; set; }
        public int PuestoId { get; set; }
        public string Nombre { get; set; }
        public string ApellidoMaterno { get; set; }
        public string ApellidoPaterno { get; set; }
        public bool? Activo { get; set; }
        public bool? Sesion { get; set; }
        public string Psd { get; set; }

        public Puestos Puesto { get; set; }
        public ICollection<OperadorPlaza> OperadorPlaza { get; set; }
    }
}
