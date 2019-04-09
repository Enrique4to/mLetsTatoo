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

        public System.Data.Entity.DbSet<mLetsTatoo.Models.T_citaimagenes> T_citaimagenes { get; set; }

        public System.Data.Entity.DbSet<mLetsTatoo.Models.T_ciudad> T_ciudad { get; set; }

        public System.Data.Entity.DbSet<mLetsTatoo.Models.T_clientes> T_clientes { get; set; }

        public System.Data.Entity.DbSet<mLetsTatoo.Models.T_empresas> T_empresas { get; set; }

        public System.Data.Entity.DbSet<mLetsTatoo.Models.T_estado> T_estado { get; set; }

        public System.Data.Entity.DbSet<mLetsTatoo.Models.T_localfacturas> T_localfacturas { get; set; }

        public System.Data.Entity.DbSet<mLetsTatoo.Models.T_localhistoria> T_localhistoria { get; set; }

        public System.Data.Entity.DbSet<mLetsTatoo.Models.T_localhorarios> T_localhorarios { get; set; }

        public System.Data.Entity.DbSet<mLetsTatoo.Models.T_localimagenes> T_localimagenes { get; set; }

        public System.Data.Entity.DbSet<mLetsTatoo.Models.T_locales> T_locales { get; set; }

        public System.Data.Entity.DbSet<mLetsTatoo.Models.T_locatarios> T_locatarios { get; set; }

        public System.Data.Entity.DbSet<mLetsTatoo.Models.T_postal> T_postal { get; set; }

        public System.Data.Entity.DbSet<mLetsTatoo.Models.T_propietarios> T_propietarios { get; set; }

        public System.Data.Entity.DbSet<mLetsTatoo.Models.T_redes> T_redes { get; set; }

        public System.Data.Entity.DbSet<mLetsTatoo.Models.T_tecimagenes> T_tecimagenes { get; set; }

        public System.Data.Entity.DbSet<mLetsTatoo.Models.T_tecnicos> T_tecnicos { get; set; }

        public System.Data.Entity.DbSet<mLetsTatoo.Models.T_trabajocitas> T_trabajocitas { get; set; }

        public System.Data.Entity.DbSet<mLetsTatoo.Models.T_trabajonota> T_trabajonota { get; set; }

        public System.Data.Entity.DbSet<mLetsTatoo.Models.T_trabajos> T_trabajos { get; set; }

        public System.Data.Entity.DbSet<mLetsTatoo.Models.T_usuarios> T_usuarios { get; set; }

        public System.Data.Entity.DbSet<mLetsTatoo.Common.Models.T_teccaract> T_teccaract { get; set; }
    }
}
