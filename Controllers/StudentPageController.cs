using HTTP_5125_Cumulative1.Models;
using Microsoft.AspNetCore.Mvc;

namespace HTTP_5125_Cumulative1.Controllers
{
    public class StudentPageController : Controller
    {
        private readonly StudentAPIController _api;

        public StudentPageController(StudentAPIController api)
        {
            _api = api;
        }





        /// <summary>
        /// Retrieves the list of students by calling the <see cref="StudentAPIController.ListStudents"/> method 
        /// and passes the list to the view for rendering.
        /// </summary>
        /// <returns>A view that displays a list of students.</returns>
        /// <example>
        /// Example URL to call the endpoint to retrieve the list of students: 
        /// GET /api/StudentAPI/ListStudents
        /// </example>
        public IActionResult List()
        {
            // Get the list of students from the API controller
            List<Student> students = _api.ListStudents();

            // Pass the list to the view
            return View(students);
        }
    }
}
