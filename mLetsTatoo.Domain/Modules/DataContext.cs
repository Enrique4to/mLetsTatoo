namespace mLetsTatoo.Domain.Modules
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public class DataContext : DbContext
    {
        public DataContext() : base("CS")
        {

        }

        public System.Data.Entity.DbSet<mLetsTatoo.Models.Tcita_imagenes> Tcita_imagenes { get; set; }

        public System.Data.Entity.DbSet<mLetsTatoo.Models.Tciudad> Tciudads { get; set; }

        public System.Data.Entity.DbSet<mLetsTatoo.Models.Tclientes> Tclientes { get; set; }

        public System.Data.Entity.DbSet<mLetsTatoo.Models.Tempresas> Tempresas { get; set; }

        public System.Data.Entity.DbSet<mLetsTatoo.Models.Testado> Testadoes { get; set; }

        public System.Data.Entity.DbSet<mLetsTatoo.Models.Tlocal_facturas> Tlocal_facturas { get; set; }

        public System.Data.Entity.DbSet<mLetsTatoo.Models.Tlocal_historia> Tlocal_historia { get; set; }

        public System.Data.Entity.DbSet<mLetsTatoo.Models.Tlocal_horarios> Tlocal_horarios { get; set; }

        public System.Data.Entity.DbSet<mLetsTatoo.Models.Tlocal_imagenes> Tlocal_imagenes { get; set; }

        public System.Data.Entity.DbSet<mLetsTatoo.Models.Tlocales> Tlocales { get; set; }

        public System.Data.Entity.DbSet<mLetsTatoo.Models.Tlocatarios> Tlocatarios { get; set; }

        public System.Data.Entity.DbSet<mLetsTatoo.Models.Tpostal> Tpostals { get; set; }

        public System.Data.Entity.DbSet<mLetsTatoo.Models.Tpropietarios> Tpropietarios { get; set; }

        public System.Data.Entity.DbSet<mLetsTatoo.Models.Tredes> Tredes { get; set; }

        public System.Data.Entity.DbSet<mLetsTatoo.Models.Ttec_imagenes> Ttec_imagenes { get; set; }

        public System.Data.Entity.DbSet<mLetsTatoo.Models.Ttecnicos> Ttecnicos { get; set; }

        public System.Data.Entity.DbSet<mLetsTatoo.Models.Ttrabajo_citas> Ttrabajo_citas { get; set; }

        public System.Data.Entity.DbSet<mLetsTatoo.Models.Ttrabajo_nota> Ttrabajo_nota { get; set; }

        public System.Data.Entity.DbSet<mLetsTatoo.Models.Ttrabajos> Ttrabajos { get; set; }

        public System.Data.Entity.DbSet<mLetsTatoo.Models.Tusuarios> Tusuarios { get; set; }
    }
}
