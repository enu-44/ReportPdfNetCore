using System.Collections.Generic;

namespace pmacore_api.Models.pma.excell
{
    public class Viatico
    {
        public string FechaInicial { get; set; }
        public string FechaFinal { get; set; }
        public string Relacion { get; set; }
        public string Contrato { get; set; }
        public string Base { get; set; }
        public List<DetalleViatico> Data { get; set; }
    }

    public class DetalleViatico
    {
        public string Consecutivo { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Cargo { get; set; }
        public string Sucursal { get; set; }
        public string FechaInicial { get; set; }
        public string FechaFinal { get; set; }
        public string Orden { get; set; }
        public string TotalDias { get; set; }
        public string Incidencia { get; set; }
        public string ViaticoPermanente { get; set; }
        public string ViaticoOcasional { get; set; }
        public string SaldoAnticipo { get; set; }
        public string DescAlim { get; set; }
        public string DescTran { get; set; }
    }
}