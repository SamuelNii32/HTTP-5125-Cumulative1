namespace HTTP_5125_Cumulative1.Models
{
    public class TeacherWithCoursesViewModel
    {
        // Property to store the Teacher object
        public Teacher Teacher { get; set; }

        // Property to store the list of courses taught by the teacher
        public List<Course> Courses { get; set; }
    }
}
