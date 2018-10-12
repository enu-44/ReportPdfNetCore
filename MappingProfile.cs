using AutoMapper;
using pmacore_api.Models.datatake.equipos;

namespace pmacore_api
{
    public class MappingProfile:Profile {
        public MappingProfile() {
            // Add as many of these lines as you need to map your objects
        
            //CreateMap<EmpresaViewModel, Empresa>();
            CreateMap<ViewEquipos, ViewEquiposMap>();
            CreateMap<ViewEquiposMap, ViewEquipos>();
            
        
        }
    }
}