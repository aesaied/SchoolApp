using System.ComponentModel.DataAnnotations;

namespace SchoolApp.Data
{
#nullable disable
    public class Course
    {
        public int CourseId {  get; set; }

        [StringLength(100, MinimumLength =2)]
        public string Title { get; set; }

        [Range(0, 10)]
       
        public int Credits { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }

    }
}
