using Microsoft.EntityFrameworkCore;

namespace SchoolApp.Data
{
    public class SchoolContext : DbContext
    {

        public SchoolContext(DbContextOptions<SchoolContext> options):base(options)
        {
            
        }


        public DbSet<Course> Courses { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<Enrollment> Enrollments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Course>().HasMany(c=>c.Enrollments).WithOne(s=> s.Course).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Student>().HasMany(c => c.Enrollments).WithOne(s => s.Student).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Course>().HasData(
                new Course() { CourseId=1, Title="C++", Credits=3 },
                new Course() { CourseId = 2, Title = "C++ Lab", Credits = 1 },
                new Course() { CourseId = 3, Title = "Java", Credits = 3 },
                new Course() { CourseId = 4, Title = "Arabic", Credits = 3 }
                );


            modelBuilder.Entity<Student>().HasData(
                new Student() { StudentId = 1, FirstName = "Wael", LastName = "Salameh" },
                new Student() { StudentId = 2, FirstName = "Bisan", LastName = "Husain" }
                );

            modelBuilder.Entity<Enrollment>().HasData(

                new Enrollment() { EnrollmentId = 1, StudentId = 1, CourseId = 1 },
                new Enrollment() { EnrollmentId = 2, StudentId = 1, CourseId = 2 },
                new Enrollment() { EnrollmentId = 3, StudentId = 2, CourseId = 3 },
                new Enrollment() { EnrollmentId = 4, StudentId = 2, CourseId = 4 }



                );
        }


    }
}
