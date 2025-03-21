
using Microsoft.AspNetCore.Mvc;
using HTTP_5125_Cumulative1.Models;
using MySql.Data.MySqlClient;

namespace HTTP_5125_Cumulative1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherAPIController : ControllerBase
    {
        // Dependency Injection
        private readonly SchoolDbContext _context;

        public TeacherAPIController(SchoolDbContext context)
        {
            _context = context;
        }


        ///<summary>
        /// Retrieves a list of teachers who were hired within a specified date range.
        ///</summary>
        ///<param name="startDate">The start date of the hiring range.</param>
        ///<param name="endDate">The end date of the hiring range.</param>
        ///<returns>A list of teachers hired within the specified date range.</returns>
        ///<example>
        /// GET api/teacherapi/ListTeachersByHireDateRange?startDate=2014-06-10&endDate=2016-06-10
        /// -> [{"teacherId":2,"firstName":"Caitlin","lastName":"Cummings","employeeNumber":"T381","hireDate":"2014-06-10T00:00:00","salary":62.77},
        /// {"teacherId":3,"firstName":"Linda","lastName":"Chan","employeeNumber":"T382","hireDate":"2015-08-22T00:00:00","salary":60.22},
        /// {"teacherId":4,"firstName":"Lauren","lastName":"Smith","employeeNumber":"T385","hireDate":"2014-06-22T00:00:00","salary":74.20},
        /// {"teacherId":8,"firstName":"Dana","lastName":"Ford","employeeNumber":"T401","hireDate":"2014-06-26T00:00:00","salary":71.15},
        /// {"teacherId":10,"firstName":"John","lastName":"Taram","employeeNumber":"T505","hireDate":"2015-10-23T00:00:00","salary":79.63}]
        ///</example>

        [HttpGet("ListTeachersByHireDateRange")]
        public List<Teacher> ListTeachersByHireDateRange(DateTime startDate, DateTime endDate)
        {
            // List to store teacher details
            List<Teacher> Teachers = new List<Teacher>();

            // Create a database connection
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                // Open connection
                Connection.Open();

                // Create a command
                MySqlCommand Command = Connection.CreateCommand();

                // Query to fetch teachers within the specified hire date range
                Command.CommandText = "SELECT * FROM teachers WHERE hiredate BETWEEN @startDate AND @endDate";
                Command.Parameters.AddWithValue("@startDate", startDate);
                Command.Parameters.AddWithValue("@endDate", endDate);
               

                // Execute the query and get results
                using (MySqlDataReader ResultSet = Command.ExecuteReader())
                {
                    // Loop through the result set
                    while (ResultSet.Read())
                    {
                        // Create a teacher object with all details
                        Teacher teacher = new Teacher
                        {
                            TeacherId = Convert.ToInt32(ResultSet["teacherid"]),
                            FirstName = ResultSet["teacherfname"].ToString(),
                            LastName = ResultSet["teacherlname"].ToString(),
                            EmployeeNumber = ResultSet["employeenumber"].ToString(),
                            HireDate = Convert.ToDateTime(ResultSet["hiredate"]),
                            Salary = Convert.ToDecimal(ResultSet["salary"])
                        };

                        // Add the teacher to the list
                        Teachers.Add(teacher);
                    }
                }
            }

            // Return the list of teachers with all their details
            return Teachers;
        }



        ///<summary>
        /// Gets all the teachers from the schooldb database with all their details.
        ///</summary>
        ///<returns>A list of teachers with all their details</returns>
        ///<example>
        ///GET api/teacherapi/ListTeachers -> [{"teacherId":1,"firstName":"Alexander","lastName":"Bennett","employeeNumber":"T378","hireDate":"2016-08-05T00:00:00","salary":55.30},
        ///{"teacherId":2,"firstName":"Caitlin","lastName":"Cummings","employeeNumber":"T381","hireDate":"2014-06-10T00:00:00","salary":62.77},
        ///{"teacherId":3,"firstName":"Linda","lastName":"Chan","employeeNumber":"T382","hireDate":"2015-08-22T00:00:00","salary":60.22}
        ///,{"teacherId":4,"firstName":"Lauren","lastName":"Smith","employeeNumber":"T385","hireDate":"2014-06-22T00:00:00","salary":74.20},
        ///{"teacherId":5,"firstName":"Jessica","lastName":"Morris","employeeNumber":"T389","hireDate":"2012-06-04T00:00:00","salary":48.62},
        ///{"teacherId":6,"firstName":"Thomas","lastName":"Hawkins","employeeNumber":"T393","hireDate":"2016-08-10T00:00:00","salary":54.45},
        ///{"teacherId":7,"firstName":"Shannon","lastName":"Barton","employeeNumber":"T397","hireDate":"2013-08-04T00:00:00","salary":64.70},{"teacherId":8,"firstName":"Dana","lastName":"Ford","employeeNumber":"T401","hireDate":"2014-06-26T00:00:00","salary":71.15},
        ///{"teacherId":9,"firstName":"Cody","lastName":"Holland","employeeNumber":"T403","hireDate":"2016-06-13T00:00:00","salary":43.20},
        ///{"teacherId":10,"firstName":"John","lastName":"Taram","employeeNumber":"T505","hireDate":"2015-10-23T00:00:00","salary":79.63}]
        ///</example>

        [HttpGet("ListTeachers")]
        public List<Teacher> ListTeachers()
        {
            // List to store all teacher details
            List<Teacher> Teachers = new List<Teacher>();

            // Create a database connection
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                // Open connection
                Connection.Open();

                // Create a command
                MySqlCommand Command = Connection.CreateCommand();

                // Query to fetch all teachers
                Command.CommandText = "SELECT * FROM teachers";

                // Execute the query and get results
                using (MySqlDataReader ResultSet = Command.ExecuteReader())
                {
                    // Loop through the result set
                    while (ResultSet.Read())
                    {
                        // Create a teacher object with all details
                        Teacher teacher = new Teacher
                        {
                            TeacherId = Convert.ToInt32(ResultSet["teacherid"]),
                            FirstName = ResultSet["teacherfname"].ToString(),
                            LastName = ResultSet["teacherlname"].ToString(),
                            EmployeeNumber = ResultSet["employeenumber"].ToString(),
                            HireDate = Convert.ToDateTime(ResultSet["hiredate"]),
                            Salary = Convert.ToDecimal(ResultSet["salary"])
                        };

                        // Add the teacher to the list
                        Teachers.Add(teacher);
                    }
                }
            }

            // Return the list of all teachers
            return Teachers;
        }


        /// <summary>
        /// This is going to receive the teacher Id as input and output the associated teacher's full details.
        /// </summary>
        /// <param name="TeacherId">The primary key of the teacher</param>
        /// <returns>A teacher object containing all their details</returns>
        /// <example>
        /// GET api/teacherapi/FindTeacher/1 -> { "TeacherId": 1, "FirstName": "Alexander", "LastName": "Bennett", "EmployeeNumber": "T378", "HireDate": "2016-08-05", "Salary": 55.30 }
        /// GET api/teacherapi/FindTeacher/999 -> null (or 404 Not Found) if the teacher ID does not exist in the database.
        /// </example>
        [HttpGet]
        [Route("FindTeacher/{id}")]
        public Teacher FindTeacher(int id)
        {
            // Initialize SelectedTeacher as null 
            Teacher SelectedTeacher = null;

            // 'using' will close the connection after the code executes
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                // Open the connection
                Connection.Open();

                // Establish a new command (query) for our database
                MySqlCommand Command = Connection.CreateCommand();

                // @id is replaced with a 'sanitized' id
                Command.CommandText = "SELECT * FROM teachers WHERE teacherid=@id";
                Command.Parameters.AddWithValue("@id", id);

                // Gather Result Set of Query into a variable
                using (MySqlDataReader ResultSet = Command.ExecuteReader())
                {
                    // Check if a row is returned
                    if (ResultSet.Read())
                    {
                        // Access column information by the DB column name as an index
                        int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                        string FirstName = ResultSet["teacherfname"].ToString();
                        string LastName = ResultSet["teacherlname"].ToString();
                        string EmployeeNumber = ResultSet["employeenumber"].ToString();
                        DateTime HireDate = Convert.ToDateTime(ResultSet["hiredate"]);
                        decimal Salary = Convert.ToDecimal(ResultSet["salary"]);

                        // Populate the teacher object with the retrieved data
                        SelectedTeacher = new Teacher
                        {
                            TeacherId = TeacherId,
                            FirstName = FirstName,
                            LastName = LastName,
                            EmployeeNumber = EmployeeNumber,
                            HireDate = HireDate,
                            Salary = Salary
                        };
                    }
                }
            }

            // Return the teacher object (null if not found)
            return SelectedTeacher;

        }

        ///<summary>
        /// Retrieves a list of courses taught by a specific teacher.
        ///</summary>
        ///<param name="teacherId">The ID of the teacher whose courses are to be listed.</param>
        ///<returns>A list of courses assigned to the specified teacher.</returns>
        ///<example>
        /// GET api/teacherapi/ListCoursesByTeacherId/1 
        /// -> [{"courseId":1,"courseCode":"http5101","teacherId":1,"startDate":"2018-09-04T00:00:00","finishDate":"2018-12-14T00:00:00","courseName":"Web Application Development","teacher":null}]
        ///</example>

        [HttpGet("ListCoursesByTeacherId/{teacherId}")]
        public List<Course> ListCoursesByTeacherId(int teacherId)
        {
            List<Course> courses = new List<Course>();

            using (MySqlConnection connection = _context.AccessDatabase())
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();

                // Query to fetch courses taught by the teacher with the given teacherId
                command.CommandText = "SELECT * FROM courses WHERE teacherid = @teacherId";
                command.Parameters.AddWithValue("@teacherId", teacherId);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Populate the Course object
                        Course course = new Course
                        {
                            CourseId = Convert.ToInt32(reader["courseid"]),
                            CourseCode = reader["coursecode"].ToString(),
                            TeacherId = Convert.ToInt32(reader["teacherid"]),
                            StartDate = Convert.ToDateTime(reader["startdate"]),
                            FinishDate = Convert.ToDateTime(reader["finishdate"]),
                            CourseName = reader["coursename"].ToString()
                        };

                        // Add the course to the list
                        courses.Add(course);
                    }
                }
            }

            return courses;
        }



    }
}
