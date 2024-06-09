using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SchoolApp.Data;

namespace SchoolApp.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolContext _db;

        public StudentsController(SchoolContext db)
        {
            
            _db = db;
        }
        public async Task<IActionResult> Index(string?  keyword=null)
        {



            var students = _db.Students.AsQueryable();

           if (!string.IsNullOrWhiteSpace(keyword))
            {
                students= students.Where(s=>s.FirstName.Contains(keyword) || s.LastName.Contains(keyword));

            }

           var  result = await students.ToListAsync();

            ViewBag.KeyWord = keyword;
            return View(result);
        }


        public async Task<IActionResult> Details(int id) {

            if(id<=0) return NotFound();


            // load related data
            var  student = await _db.Students.Include(s=>s.Enrollments).ThenInclude(e=>e.Course).SingleOrDefaultAsync(s=>s.StudentId == id);

            if(student == null)
            {

                TempData["Error"] = "Student not found!!";
                return RedirectToAction(nameof(Index));
            }


          return View(student); 
        }

    }
}
