namespace pmacore_api.Models.datatake.equipos
{
    public class ViewEquiposMap
    {
        
        //Atributes
        private  string conectadoRbt_;
        private  string medidorBt_;
        //Model
        public long Id {get; set;}

        public string dep_nombre {get; set;}
        public long mun_id {get; set;}
        public string mun_nombre {get; set;}        
        public long? bar_id {get; set;}

        public string bar_nombre {get; set;}

        public long numero_apoyo {get; set;}

        public string codigoapoyo {get; set;}

        public long empresa_id {get; set;}
        public string empresa_nombre {get; set;}

        public long cantidad {get; set;}

        public string tipo_equipo {get; set;}

        public string ConectadoRbt {
             get
            {
                return conectadoRbt_;
            }
            set
            {
                if (value=="False")
                {
                    conectadoRbt_ = "NO";
                }else{
                    conectadoRbt_ = "SI";
                }
                

            } 
        }

        public long Consumo {get; set;}

        public string MedidorBt {
             get
            {
                return medidorBt_;
            }
            set
            {
                if (value=="False")
                {
                    medidorBt_ = "NO";
                }else{
                    medidorBt_ = "SI";
                }
                

            } 
        }

        public string Codigo {get; set;}

        public string Descripcion {get; set;}
    }
}