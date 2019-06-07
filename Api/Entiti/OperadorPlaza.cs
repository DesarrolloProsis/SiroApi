using System;
using System.Collections.Generic;

namespace Api.Models
{
    public partial class OperadorPlaza
    {
        public string NumeroCapufeOperador { get; set; }
        public string NumeroGea { get; set; }
        public string NumeroPlazaCapufe { get; set; }

        public Operadores NumeroCapufeOperadorNavigation { get; set; }
        public Plazas NumeroPlazaCapufeNavigation { get; set; }
    }
}
