using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace pmacore_api.Models.datatake.fotos
{
    [Table("Foto")]
    public class Fotos
    {
        [Column("Id")]
        public long Id {get; set;}
        [Column("Descripcion")]
        public string Descripcion {get; set;}
        [Column("Elemento_Id")]
        public long Elemento_Id {get; set;}

        [Column("FechaCreacion")]
        public DateTime FechaCreacion {get; set;}

        [Column("HoraCreacion")]
        public string HoraCreacion {get; set;}

        [Column("Novedad_Id")]
        public long? Novedad_Id {get; set;}

        [Column("Ruta")]
        public string Ruta {get; set;}

        [Column("Titulo")]
        public string Titulo {get; set;}

        [Column("TipoFoto_Id")]
        public long TipoFoto_Id {get; set;}

        
    }
}