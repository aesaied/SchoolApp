using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SchoolApp.Data
{
#nullable disable
    public class Student
    {
        [Key]  // optional  
        public int StudentId { get; set; }

        [StringLength(20,MinimumLength =2)]
        [Required]
        public string FirstName { get; set; }

        [StringLength(20, MinimumLength = 2)]
        [Required]
        public string LastName { get; set; }


        public ICollection<Enrollment> Enrollments { get; set; }


    }
}
