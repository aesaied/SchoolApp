using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolApp.Data;

namespace SchoolApp.Views.Shared.Components.RecentCourses
{
    public class RecentCoursesViewComponent: ViewComponent
    {

        private readonly SchoolContext _db;

        public RecentCoursesViewComponent(SchoolContext db)
        {
                
            _db = db;
        }


        public async Task<IViewComponentResult> InvokeAsync(int count=5)
        {

            var  courses = await _db.Courses.OrderByDescending(s=>s.CourseId).Take(count).ToListAsync();

            return View(courses);
        }
    }
}
