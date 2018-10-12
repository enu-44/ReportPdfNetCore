using System;

namespace pmacore_api.Models.datatake
{
    public class ViewElementoCables
    {
        //Atributes
        private  string tiene_marquilla_;
        private  string sobreRbt_;

        //Porperties
        public long cable_id { get; set; }
        public string nombre_cable { get; set; }
        public string sigla_cable { get; set; }
        public long tipo_cable_id { get; set; }
        public string nombre_tipo_cable { get; set; }
        public string nombre_empresa { get; set; }
        public string nit_empresa { get; set; }
        public string direccion_empresa { get; set; }
        public long elemento_cable_id { get; set; }
        public string sobrerbt { 
            get
            {
                return sobreRbt_;
            }
            set
            {
                if (value=="False")
                {
                    sobreRbt_ = "NO";
                }else{
                    sobreRbt_ = "SI";
                }
                

            } 
         }
        public string tiene_marquilla {
            get
            {
                return tiene_marquilla_;
            }
            set
            {
                if (value=="False")
                {
                    tiene_marquilla_ = "NO";
                }else{
                    tiene_marquilla_ = "SI";
                }
                

            } 
        }
        public long cantidad_cable { get; set; }
        public long empresa_id { get; set; }
        public long detalletipocable_id { get; set; }
        public long elemento_id { get; set; }
        public string codigoapoyo { get; set; }
        public DateTime fechalevantamiento { get; set; }
        public string horainicio { get; set; }
        public string imei_device { get; set; }
        public DateTime fecha_sincronizacion { get; set; }
        public string hora_sincronizacion { get; set; }
        public long estado_id { get; set; }
        public long longitud_elemento_id { get; set; }
        public long material_id { get; set; }
        public long proyecto_id { get; set; }
        public long nivel_tension_id { get; set; }
        public string horafin { get; set; }
        public string resistenciamecanica { get; set; }
        public long retenidas { get; set; }
        public double alturadisponible { get; set; }
        public long usuario_id { get; set; }
        public string nombre_estado { get; set; }
        public string sigla_estado { get; set; }
        public string nombre_material { get; set; }
        public string sigla_material { get; set; }
        public double longitud { get; set; }
        public string nombre_nivel_tension { get; set; }
        public string sigla_nivel_tension { get; set; }
        public long valor_nivel_tension { get; set; }
        public string nombre_proyecto { get; set; }
        public string nombre_usuario { get; set; }
        public string apellido_usuario { get; set; }
        public string cedula_usuario { get; set; }
        public long? barrio_id { get; set; }
        public string barrio { get; set; }
        public long ciudad_id { get; set; }
        public string ciudad { get; set; }
        public long departamento_id { get; set; }
        public string departamento { get; set; }
        public string coordenadas_elemento { get; set; }
        public string direccion_elemento { get; set; }
        public double latitud_elemento { get; set; }
        public double longitud_elemento { get; set; }
        public string direccion_gps { get; set; }
    }
}