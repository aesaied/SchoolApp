using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolApp.Data
{

    public class Enrollment
    {

        public int EnrollmentId { get; set; }

        public int StudentId { get; set; }

        public int CourseId { get; set; }

        [ForeignKey(nameof(StudentId))]
        public Student? Student { get; set; }


        [ForeignKey(nameof(CourseId))]
        public Course? Course { get; set; }

        public Grade? Grade { get; set; }
    }
}