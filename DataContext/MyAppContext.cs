using Microsoft.EntityFrameworkCore;
using pmacore_api.Models.datatake.equipos;
using pmacore_api.Models.datatake.fotos;

namespace pmacore_api.DataContext
{
    public class MyAppContext:DbContext
    {
          #region Atributes
        static DbContextOptions<MyAppContext> optionsLocal;
        static MyAppContext instance;

        #endregion

        #region 
        public virtual DbSet<ViewEquipos> ViewEquipos { get; set; }
        public virtual DbSet<Fotos> Fotos { get; set; }
    
        #endregion

        #region Singleton
        public static MyAppContext GetInstance()
        {
            if (instance == null)
            {
                instance = new MyAppContext();
            }
            return instance;
        }
        #endregion


         #region constructor
       // public MyAppContext(DbContextOptions<MyAppContext> options) : base(options)
       //{}
        public MyAppContext(DbContextOptions<MyAppContext> options) : base(options)
        {
            optionsLocal=options;
        }

        public MyAppContext() : base(optionsLocal)
        {


        }

        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        
            modelBuilder.Entity<ViewEquipos>(entity => { entity.HasKey(e => e.Id); });
            modelBuilder.Entity<Fotos>(entity => { entity.HasKey(e => e.Id); });
         
            //modelBuilder.Entity<UbicacionEmpresa>(entity => { entity.HasKey(e => e.EmpresaId, e.Ub); });
            // modelBuilder.Entity<UbicacionEmpresa>().HasKey(e =>new{e .EmpresaId,e .Ubicacion_Id});
            base.OnModelCreating(modelBuilder);
        }
    }
}