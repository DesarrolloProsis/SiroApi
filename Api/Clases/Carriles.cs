using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Clases
{
    public class Carriles
    {
        Context db = new Context();


      


        public object CarrilesPlaza(string numeroPlaza)
        {

            try
            {


                var Lista = (from c in db.Carriles                             
                             join t in db.Tramos on c.IdGare equals t.IdGare
                             where t.NumeroPlazaCapufe == numeroPlaza
                             where c.TipoCarrilId != "2"
                             select new SelectListItem
                             {
                                 Value = c.NumeroCapufeCarril.ToString(),
                                 Text = c.LineaCarril
                             }).ToList();


                List<SelectListItem> carrilesObject = new List<SelectListItem>();

                carrilesObject.Add(new SelectListItem
                {
                    Value = "null",
                    Text = "Todos los Carriles"
                });

                foreach (var item in Lista)
                {
                    

                    carrilesObject.Add(new SelectListItem
                    {
                        Value = item.Text,
                        Text = item.Text

                    });

                }

                object Json = new { carrilesObject };

                return Json;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

       

    }
    class CarrilesLinea
    {
        public int capufeCarril { get; set; }
        public string lineaCarril { get; set; }
    }
}
