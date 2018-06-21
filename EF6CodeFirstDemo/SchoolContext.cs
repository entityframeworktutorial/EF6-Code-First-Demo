using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;

namespace EF6CodeFirstDemo
{
    /// <summary>
    /// https://coding.abel.nu/2012/03/ef-migrations-command-reference/
    /// </summary>
    public class SchoolContext : DbContext
    {
        public SchoolContext() : base("SchoolDB-EF6CodeFirst")
        {
            //Database.SetInitializer<SchoolContext>(new SchoolDBInitializer());
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SchoolContext, Migrations.Configuration>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Adds configurations for Student from separate class
            modelBuilder.Configurations.Add(new StudentConfigurations());

            modelBuilder.Entity<Teacher>()
                .ToTable("TeacherInfo");

            modelBuilder.Entity<Teacher>()
                .MapToStoredProcedures();

            modelBuilder.Entity<Obv>()
                .HasKey(obv => new {obv.Typ, obv.Rok, obv.Cislo});

            modelBuilder.Entity<ObvItem>()
                .HasKey(obvItm => new {obvItm.Typ, obvItm.Rok, obvItm.Cislo, obvItm.Poradi})
                .HasRequired(obv => obv.Obv)
                .WithMany(obvItm => obvItm.Items)
                .HasForeignKey(oItm => new {oItm.Typ, oItm.Rok, oItm.Cislo});

            modelBuilder.Entity<User>()
                .Property(u => u.UserName)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<User>()
                .HasKey(u => u.Id)
                .HasIndex(u => u.UserName)
                .IsUnique();

            modelBuilder.Entity<Car>()
                .Property(c => c.Spz)
                .HasMaxLength(30)
                .IsRequired()
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("IX_Cars_Spz", 1) {IsUnique = true}
                    )
                );

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentAddress> StudentAddresses { get; set; }
        public DbSet<Obv> Obvs { get; set; }
        public DbSet<ObvItem> ObvItems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }
    }
}