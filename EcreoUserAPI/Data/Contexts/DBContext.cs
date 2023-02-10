using EcreoLibraryStandard;
using Microsoft.EntityFrameworkCore;

namespace EcreoUserAPI.Data.Contexts
{
    public class DBContext:DbContext
    {

        public DbSet<User>                  Users { get; set; }
        public DbSet<AbsenceStatus>         AbsenceStatuses { get; set; }
        public DbSet<ContactInformation>    ContactInformations { get; set; }
        public DBContext(DbContextOptions options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<AbsenceStatus>()
            //    .Navigation(x => x.User)
            //    .AutoInclude();

            builder.Entity<AbsenceStatus>()
                .HasKey(x => x.BaseID);

            builder.Entity<User>()
                .HasKey(z => z.BaseID);
            builder.Entity<User>()
                .Navigation(x => x.PersonalInformation)
                .AutoInclude();

            builder.Entity<ContactInformation>()
                .HasKey(x => x.BaseID);


        }
    }
}
