
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pmacore_api.Models.pma
{
    public class Historias
    {
       public string Fecha {get; set;}
       public string Origen {get; set;}
       public string Destino {get; set;}
       public string Tarifa {get; set;}
       public string Orden {get; set;}
       public string Estado {get; set;}
       public long Alojamiento {get; set;}
       public long Alimentacion {get; set;}
       public long Miscelaneos {get; set;}
       public long Transporte {get; set;}

        public long Tiquetes {get; set;}
        public long Terminal {get; set;}
        public string TiquetesPor {get; set;}

       public long Valor {get; set;}


    }
}