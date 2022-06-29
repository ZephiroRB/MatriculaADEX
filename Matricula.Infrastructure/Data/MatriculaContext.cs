using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace Matricula.Infrastructure.Data
{
    public class MatriculaContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<ClassRoomCourse> ClassRoomCourses { get; set; }
        public DbSet<EnrollmentDetail> EnrollmentDetails { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<ClassRoom> ClassRooms { get; set; }

        public MatriculaContext(){}

        public MatriculaContext(DbContextOptions<MatriculaContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
            var dbPort = Environment.GetEnvironmentVariable("DB_PORT");
            var dbUser = Environment.GetEnvironmentVariable("DATABASE_USER");
            var dbPassword = Environment.GetEnvironmentVariable("DATABASE_PASSWORD");
            var dbName = Environment.GetEnvironmentVariable("DB_NAME");
             
            var connectionString = $"Server={dbHost};port={dbPort};user id={dbUser};password={dbPassword};database={dbName};pooling=true";

            options.UseNpgsql(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
        }
    }
}
