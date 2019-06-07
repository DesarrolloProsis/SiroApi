using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.EntitiDTOs
{
    public class OperadorPlazaDTO
    {

        public string NumeroCapufeOperador { get; set; }
        public string NumeroGea { get; set; }
        public string NumeroPlazaCapufe { get; set; }
        public OperadoresDTO NumeroCapufeOperadorNavigation { get; set; }
        public PlazasDTO NumeroPlazaCapufeNavigation { get; set; }
    }
}
