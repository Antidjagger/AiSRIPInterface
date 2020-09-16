using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiSRIPInterface
{
    class AiSContext : DbContext
    {
        public AiSContext() : base("AiSRiPDBConnection")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<AiSContext>(null);
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Users> Users { get; set; }

        public DbSet<StatusType> StatusTypes { get; set; }
        public DbSet<Testу> Testуs { get; set; }
    }
}
