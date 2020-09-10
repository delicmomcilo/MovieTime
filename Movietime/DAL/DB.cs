using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Diagnostics.CodeAnalysis;

namespace DAL
{
    [ExcludeFromCodeCoverage]
    public class DB : DbContext
    {

        public DB() : base("Webfilm")
        {
            Database.CreateIfNotExists();
            Database.SetInitializer(new DBInit());

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public virtual DbSet<DBPoststed> Poststeder { get; set; }
        public virtual DbSet<DBKunde> Kunder { get; set; }
        public virtual DbSet<DBFilm> Filmer {get; set;}
        public virtual DbSet<DBOrdre> Ordrer { get; set; }
        public virtual DbSet<DBOrdrelinje> OrdreLinjer { get; set; }
        public virtual DbSet<DBSjanger> Sjangre { get; set; }
        public virtual DbSet<DBEndring> Endringer { get; set; }



    }
}