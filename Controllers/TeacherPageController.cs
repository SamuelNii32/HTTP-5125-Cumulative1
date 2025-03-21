using Microsoft.AspNetCore.Mvc;
using HTTP_5125_Cumulative1.Models;

namespace HTTP_5125_Cumulative1.Controllers
{
    public class TeacherPageController : Controller
    {
        // Dependency Injection to access TeacherAPIController
        private readonly TeacherAPIController _api;

        public TeacherPageController(TeacherAPIController api)
        {
            _api = api;
        }

        /// <summary>
        /// Retrieves a list of teachers based on an optional hire date range. 
        /// If no date range is provided, it fetches all teachers.
        /// </summary>
        /// <param name="startDate">The start date of the hire date range (optional).</param>
        /// <param name="endDate">The end date of the hire date range (optional).</param>
        /// <returns>A view that displays the list of teachers.</returns>
        /// <example>
        /// Example URL for retrieving the list of teachers with a hire date range:
        /// GET /TeacherPage/List?startDate=2014-06-10&endDate=2016-06-10
        /// Example URL for retrieving all teachers without a date range:
        /// GET /TeacherPage/List
        /// </example>
        public IActionResult List(DateTime? startDate, DateTime? endDate)
        {
            List<Teacher> Teachers;

            // Check if startDate and endDate are provided
            if (startDate.HasValue && endDate.HasValue)
            {
                // Fetch teachers by hire date range
                Teachers = _api.ListTeachersByHireDateRange(startDate.Value, endDate.Value);
            }
            else
            {
                // Fetch all teachers if no date range is provided
                Teachers = _api.ListTeachers();
            }

            // Pass the list of teachers to the view
            return View(Teachers);
        }

        /// <summary>
        /// Retrieves and displays the details of a specific teacher, including the courses they teach.
        /// If the teacher is not found, it returns a 404 Not Found error.
        /// </summary>
        /// <param name="id">The ID of the teacher to display.</param>
        /// <returns>A view that displays the teacher's details and courses.</returns>
        /// <example>
        /// Example URL to retrieve details for a specific teacher:
        /// GET /TeacherPage/Show/{id}
        /// Example URL: GET /TeacherPage/Show/1
        /// </example>
        public IActionResult Show(int id)
        {
            // Fetch the selected teacher by ID from the API
            Teacher SelectedTeacher = _api.FindTeacher(id);

            // If teacher is not found, return 404 Not Found response
            if (SelectedTeacher == null)
            {
                return NotFound();  // Returns a 404 HTTP status
            }

            // Fetch the courses taught by the teacher
            List<Course> Courses = _api.ListCoursesByTeacherId(id);

            // Create a ViewModel to pass both Teacher and Courses
            var viewModel = new TeacherWithCoursesViewModel
            {
                Teacher = SelectedTeacher,
                Courses = Courses
            };

            // Pass the ViewModel to the view
            return View(viewModel);
        }
    }
}
