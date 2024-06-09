using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SchoolApp.Data;
using SchoolApp.Models;

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


        [HttpGet]
        public IActionResult Create() {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(StudentCreateModel input)
        {

            if (ModelState.IsValid)
            {

                var exists =await _db.Students
                    .Where(s => s.FirstName == input.FirstName && s.LastName == input.LastName)
                    .AnyAsync();

                if (exists) {

                    ModelState.AddModelError("", "This name already registered!!");
                    return View(input);
                }

                Student student = new Student() { FirstName = input.FirstName, LastName = input.LastName };
                _db.Students.Add(student);

                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));

            }

            return View(input);
        }



        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var  student =await _db.Students.FindAsync(id);

            if (student == null) {

                TempData["Error"] = $"Student with id = {id} is not found!";
                return RedirectToAction(nameof(Index));
            }

            var model = new StudentCreateModel() { Id = student.StudentId, FirstName = student.FirstName, LastName = student.LastName };
            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(StudentCreateModel input)
        {
            if (ModelState.IsValid && input.Id.HasValue)
            {


                var exists = await _db.Students
                  .Where(s => s.FirstName == input.FirstName && s.LastName == input.LastName)
                  .Where(s=>s.StudentId !=input.Id)
                  .AnyAsync();

                if (exists)
                {

                    ModelState.AddModelError("", "This name already registered!!");
                    return View(input);
                }




                Student student = new Student() { StudentId= input.Id.Value, FirstName = input.FirstName, LastName = input.LastName };
                _db.Students.Update(student);

                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));


            }


            return View(input);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {

            var hasEnrollments = await _db.Enrollments.AnyAsync(s=>s.StudentId == id);

            if (hasEnrollments)
            {
                TempData["Error"] = "Unable to delete this row!";

                return RedirectToAction(nameof(Index));
            }


            Student student = new Student() { StudentId =id };
            _db.Students.Remove(student);

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }



    }
}
