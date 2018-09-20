
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pmacore_api.Models;

namespace pmacore_api.Models.pma
{
    public class DataRequest
    {
       public string Fecha {get; set;}
       public string Objeto {get; set;}
       public string FechaInicio {get; set;}
       public string FechaFin {get; set;}
       public string Consecutivo {get; set;}
       public Cliente Cliente {get; set;}
       public Empleado Empleado {get; set;}
       public List<Historias> Historias {get; set;}
       public long SumAloj {get; set;}
       public long SumAlim {get; set;}
       public long SumMisc {get; set;}
       public long SumTran {get; set;}
       public long SumValor {get; set;}

        public long ViaticosPMA {get; set;}
        public long ViaticosTrab {get; set;}

        ///Legalizacion
        public long DiasPernoc {get; set;}
        public long DiasRetorn {get; set;}
        public long PernocValor {get; set;}
        public long RetornValor {get; set;}
        public long ValorTotal {get; set;}
        public long SaldoFavor {get; set;}
        public string Observaciones {get; set;}
    }
}