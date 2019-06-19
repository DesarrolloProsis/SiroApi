using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.EntitiDTOs
{
    public class PlazasDTO
    {

        public string NumeroPlazaCapufe { get; set; }
        public string DelegacionId { get; set; }
        public string NombrePlaza { get; set; }
        public string IpServidor { get; set; }
        public DelegacionesDTO Delegacion { get; set; }
        public List<OperadorPlazaDTO> OperadorPlaza { get; set; }
        public List<TramosDTO> Tramos { get; set; }
  
    }

   
}
