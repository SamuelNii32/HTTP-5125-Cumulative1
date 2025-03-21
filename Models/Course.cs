namespace HTTP_5125_Cumulative1.Models
{
    // Represents a Course that a teacher can teach
    public class Course
    {
        // Unique identifier for the course
        public int CourseId { get; set; }

        // The course's code 
        public string CourseCode { get; set; }

        // The TeacherId is a foreign key that links this course to a specific teacher
        public int TeacherId { get; set; }

        // The start date of the course
        public DateTime StartDate { get; set; }

        // The finish date of the course
        public DateTime FinishDate { get; set; }

        // The name of the course (e.g., 'Introduction to Programming')
        public string CourseName { get; set; }

        // Navigation property: Represents the teacher who is teaching the course
        // It helps in creating a relationship between the Course and Teacher entities
        public Teacher Teacher { get; set; }
    }
}
