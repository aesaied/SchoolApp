    using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolApp.Data;
using SchoolApp.Models;

namespace SchoolApp.Controllers
{
    public class EnrollmentsController : Controller
    {

        private readonly SchoolContext _db;

        public EnrollmentsController(SchoolContext db)
        {
            _db = db;
                
        }
        public  async Task<IActionResult> Index()
        {

            // select  *  from students
            //select Id , firstName + ' '+ LastName as Name from students
            var students =await _db.Students.Select(s=> new { Id=s.StudentId, Name=$"{s.FirstName} {s.LastName}" }).ToListAsync();
            var courses = await _db.Courses.Select(s => new { Id = s.CourseId, Name = $"{s.Title}" }).ToListAsync();


            SelectList lst = new SelectList(students, "Id", "Name");
            SelectList lstCourses = new SelectList(courses, "Id", "Name");
            ViewBag.Students = lst;
            ViewBag.Courses = lstCourses;

            return View();
        }

        public async     Task< PartialViewResult> StudentEnrollmentDetails(int id)
        {

            var data = await _db.Enrollments
                .Where(e=>e.StudentId==id)
                .Select(s => new StudentCoursesModel() { EnrollmentId= s.EnrollmentId, CourseTitle=s.Course.Title })
                .ToListAsync();

            return PartialView("_StudentCourses", data);   
        }

        public async Task<PartialViewResult> CourseEnrollmentDetails(int id)
        {

            var data = await _db.Enrollments
                .Where(e => e.CourseId == id)
                .Select(s => new CourseStudentsListModel() { EnrollmentId = s.EnrollmentId,  StudentFullName = $"{s.Student.FirstName} {s.Student.LastName}" })
                .ToListAsync();

            return PartialView("_CourseStudents", data);
        }
    }
}
