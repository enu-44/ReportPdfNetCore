
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pmacore_api.Models;

namespace pmacore_api.Models.pma
{
    public class ResponseApiPma
    {
        public string Formato { get; set; }
        public string Version { get; set; }
        public string Email { get; set; }

        public string Fecha { get; set; }
        public string Hora { get; set; }

     
       
        public  List<DataRequest> Data {get;set;}
    }
}
