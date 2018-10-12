using System.ComponentModel.DataAnnotations.Schema;

namespace pmacore_api.Models.datatake.equipos
{
    [Table("viewequipos")]
    public class ViewEquipos
    {
        [Column("Id")]
        public long Id {get; set;}

        [Column("dep_nombre")]
        public string dep_nombre {get; set;}

        [Column("mun_id")]
        public long mun_id {get; set;}

        [Column("mun_nombre")]
        public string mun_nombre {get; set;}

        [Column("bar_id")]
        public long? bar_id {get; set;}

        [Column("bar_nombre")]
        public string bar_nombre {get; set;}

        [Column("numero_apoyo")]
        public long numero_apoyo {get; set;}

        [Column("codigoapoyo")]
        public string codigoapoyo {get; set;}

        [Column("empresa_id")]
        public long empresa_id {get; set;}

        [Column("empresa_nombre")]
        public string empresa_nombre {get; set;}

        [Column("cantidad")]
        public long cantidad {get; set;}

        [Column("tipo_equipo")]
        public string tipo_equipo {get; set;}

         [Column("ConectadoRbt")]
        public bool ConectadoRbt {get; set;}

        [Column("Consumo")]
        public long Consumo {get; set;}

        [Column("MedidorBt")]
        public bool MedidorBt {get; set;}

        [Column("Codigo")]
        public string Codigo {get; set;}

        [Column("Descripcion")]
        public string Descripcion {get; set;}


    }
}