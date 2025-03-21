namespace HTTP_5125_Cumulative1.Models
{
    public class Teacher
    {
        public int TeacherId { get; set; } // Unique identifier for the teacher
        public string FirstName { get; set; } // Teacher's first name
        public string LastName { get; set; } // Teacher's last name
        public string EmployeeNumber { get; set; } // Unique employee number assigned to the teacher
        public DateTime HireDate { get; set; } // Date the teacher was hired
        public decimal Salary { get; set; } // Teacher's salary
    }
}
