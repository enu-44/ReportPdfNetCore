using System;

namespace pmacore_api.Models.datatake
{
    public class ViewElementoCables
    {
        public int cable_id { get; set; }
        public string nombre_cable { get; set; }
        public string sigla_cable { get; set; }
        public int tipo_cable_id { get; set; }
        public string nombre_tipo_cable { get; set; }
        public string nombre_empresa { get; set; }
        public string nit_empresa { get; set; }
        public string direccion_empresa { get; set; }
        public int elemento_cable_id { get; set; }
        public bool sobrerbt { get; set; }
        public bool tiene_marquilla { get; set; }
        public int cantidad_cable { get; set; }
        public int empresa_id { get; set; }
        public int detalletipocable_id { get; set; }
        public int elemento_id { get; set; }
        public object codigoapoyo { get; set; }
        public DateTime fechalevantamiento { get; set; }
        public string horainicio { get; set; }
        public string imei_device { get; set; }
        public DateTime fecha_sincronizacion { get; set; }
        public string hora_sincronizacion { get; set; }
        public int estado_id { get; set; }
        public int longitud_elemento_id { get; set; }
        public int material_id { get; set; }
        public int proyecto_id { get; set; }
        public int nivel_tension_id { get; set; }
        public string horafin { get; set; }
        public string resistenciamecanica { get; set; }
        public int retenidas { get; set; }
        public double alturadisponible { get; set; }
        public int usuario_id { get; set; }
        public string nombre_estado { get; set; }
        public string sigla_estado { get; set; }
        public string nombre_material { get; set; }
        public string sigla_material { get; set; }
        public double longitud { get; set; }
        public string nombre_nivel_tension { get; set; }
        public string sigla_nivel_tension { get; set; }
        public int valor_nivel_tension { get; set; }
        public string nombre_proyecto { get; set; }
        public string nombre_usuario { get; set; }
        public string apellido_usuario { get; set; }
        public string cedula_usuario { get; set; }
        public int barrio_id { get; set; }
        public string barrio { get; set; }
        public int ciudad_id { get; set; }
        public string ciudad { get; set; }
        public int departamento_id { get; set; }
        public string departamento { get; set; }
        public string coordenadas_elemento { get; set; }
        public string direccion_elemento { get; set; }
        public double latitud_elemento { get; set; }
        public double longitud_elemento { get; set; }
        public string direccion_gps { get; set; }
    }
}