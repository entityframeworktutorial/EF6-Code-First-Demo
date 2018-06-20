using System.Data.Entity;

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
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentAddress> StudentAddresses { get; set; }
        public DbSet<Obv> Obvs { get; set; }
    }
}