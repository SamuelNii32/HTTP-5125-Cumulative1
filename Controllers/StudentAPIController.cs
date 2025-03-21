using HTTP_5125_Cumulative1.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;

namespace HTTP_5125_Cumulative1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentAPIController : ControllerBase
    {

        // Dependency Injection

        private readonly SchoolDbContext _context;

        public StudentAPIController(SchoolDbContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Retrieves a list of all students in the database.
        /// </summary>
        /// <returns>A list of all students with their details.</returns>
        /// <example>
        /// GET api/studentapi/ListStudents
        /// [{"studentId":1,"firstName":"Sarah","lastName":"Valdez","studentNumber":"N1678","enrollDate":"2018-06-18T00:00:00"},
        /// {"studentId":2,"firstName":"Jennifer","lastName":"Faulkner","studentNumber":"N1679","enrollDate":"2018-08-02T00:00:00"},
        /// {"studentId":3,"firstName":"Austin","lastName":"Simon","studentNumber":"N1682","enrollDate":"2018-06-14T00:00:00"},
        /// {"studentId":4,"firstName":"Mario","lastName":"English","studentNumber":"N1686","enrollDate":"2018-07-03T00:00:00"},
        /// {"studentId":5,"firstName":"Elizabeth","lastName":"Murray","studentNumber":"N1690","enrollDate":"2018-07-12T00:00:00"},{"studentId":6,"firstName":"Kevin","lastName":"Williams","studentNumber":"N1691","enrollDate":"2018-08-04T00:00:00"},
        /// {"studentId":7,"firstName":"Jason","lastName":"Freeman","studentNumber":"N1694","enrollDate":"2018-08-16T00:00:00"},{"studentId":8,"firstName":"Nicole","lastName":"Armstrong","studentNumber":"N1698","enrollDate":"2018-07-10T00:00:00"},{"studentId":9,"firstName":"Colleen","lastName":"Riley","studentNumber":"N1702","enrollDate":"2018-07-15T00:00:00"},{"studentId":10,"firstName":"Julie","lastName":"Salazar","studentNumber":"N1705","enrollDate":"2018-07-10T00:00:00"},{"studentId":11,"firstName":"Dr.","lastName":"Bridges","studentNumber":"N1709","enrollDate":"2018-08-22T00:00:00"},{"studentId":12,"firstName":"Vanessa","lastName":"Cox","studentNumber":"N1712","enrollDate":"2018-08-17T00:00:00"},{"studentId":13,"firstName":"Denise","lastName":"Jackson","studentNumber":"N1714","enrollDate":"2018-07-26T00:00:00"},{"studentId":14,"firstName":"Roy","lastName":"Davidson","studentNumber":"N1715","enrollDate":"2018-08-11T00:00:00"},{"studentId":15,"firstName":"Ryan","lastName":"Walters","studentNumber":"N1717","enrollDate":"2018-07-25T00:00:00"},{"studentId":16,"firstName":"Patricia","lastName":"Sweeney","studentNumber":"N1719","enrollDate":"2018-08-08T00:00:00"},{"studentId":18,"firstName":"Melissa","lastName":"Morales","studentNumber":"N1723","enrollDate":"2018-08-10T00:00:00"},{"studentId":19,"firstName":"Kimberly","lastName":"Johnson","studentNumber":"N1727","enrollDate":"2018-08-02T00:00:00"},{"studentId":20,"firstName":"Andrea","lastName":"Flores","studentNumber":"N1731","enrollDate":"2018-07-09T00:00:00"},{"studentId":21,"firstName":"Jason","lastName":"II","studentNumber":"N1732","enrollDate":"2018-06-05T00:00:00"},{"studentId":22,"firstName":"David","lastName":"Dunlap","studentNumber":"N1734","enrollDate":"2018-08-27T00:00:00"},{"studentId":23,"firstName":"Elizabeth","lastName":"Thompson","studentNumber":"N1736","enrollDate":"2018-08-07T00:00:00"},{"studentId":24,"firstName":"Becky","lastName":"Medina","studentNumber":"N1737","enrollDate":"2018-07-02T00:00:00"},{"studentId":25,"firstName":"Wayne","lastName":"Collins","studentNumber":"N1740","enrollDate":"2018-07-20T00:00:00"},{"studentId":26,"firstName":"Nicole","lastName":"Henderson","studentNumber":"N1742","enrollDate":"2018-06-07T00:00:00"},{"studentId":27,"firstName":"David","lastName":"Larson","studentNumber":"N1744","enrollDate":"2018-07-19T00:00:00"},{"studentId":28,"firstName":"John","lastName":"Reed","studentNumber":"N1748","enrollDate":"2018-08-15T00:00:00"},{"studentId":29,"firstName":"Richard","lastName":"King","studentNumber":"N1751","enrollDate":"2018-08-17T00:00:00"},{"studentId":30,"firstName":"Alexander","lastName":"Bennett","studentNumber":"N1752","enrollDate":"2018-07-29T00:00:00"},{"studentId":31,"firstName":"Caitlin","lastName":"Cummings","studentNumber":"N1756","enrollDate":"2018-08-02T00:00:00"},{"studentId":32,"firstName":"Christine","lastName":"Bittle","studentNumber":"N0001","enrollDate":"2020-10-05T00:00:00"}]
        /// </example>

        [HttpGet("ListStudents")]
        public List<Student> ListStudents()
        {
            List<Student> students = new List<Student>();

            // Create a database connection
            using (MySqlConnection connection = _context.AccessDatabase())
            {
                // Open the connection
                connection.Open();

                // Create a command
                MySqlCommand command = connection.CreateCommand();

                // Query to fetch all students
                command.CommandText = "SELECT * FROM students";

                // Execute the query and get results
                using (MySqlDataReader resultSet = command.ExecuteReader())
                {
                    // Loop through the result set
                    while (resultSet.Read())
                    {
                        // Create a student object with all details
                        Student student = new Student
                        {
                            StudentId = Convert.ToInt32(resultSet["studentid"]),
                            FirstName = resultSet["studentfname"].ToString(),
                            LastName = resultSet["studentlname"].ToString(),
                            StudentNumber = resultSet["studentnumber"].ToString(),
                            EnrollDate = Convert.ToDateTime(resultSet["enroldate"])
                        };

                        // Add the student to the list
                        students.Add(student);
                    }
                }
            }

            // Return the list of students
            return students;
        }

        /// <summary>
        /// Retrieves a single student by their studentId.
        /// </summary>
        /// <param name="studentId">The unique ID of the student.</param>
        /// <returns>The details of a single student.</returns>
        /// <example>
        /// GET api/studentapi/GetStudentById/1
        /// -> {"studentId":1,"firstName":"Sarah","lastName":"Valdez","studentNumber":"N1678","enrollDate":"2018-06-18T00:00:00"}
        /// </example>
        [HttpGet("GetStudentById/{studentId}")]
        public ActionResult<Student> GetStudentById(int studentId)
        {
            // Create a database connection
            using (MySqlConnection connection = _context.AccessDatabase())
            {
                // Open the connection
                connection.Open();

                // Create a command
                MySqlCommand command = connection.CreateCommand();

                // Query to fetch the student by studentId
                command.CommandText = "SELECT * FROM students WHERE studentid = @studentId";
                command.Parameters.AddWithValue("@studentId", studentId);

                // Execute the query and get results
                using (MySqlDataReader resultSet = command.ExecuteReader())
                {
                    // Check if the student exists
                    if (resultSet.Read())
                    {
                        // Create a student object with the details
                        Student student = new Student
                        {
                            StudentId = Convert.ToInt32(resultSet["studentid"]),
                            FirstName = resultSet["studentfname"].ToString(),
                            LastName = resultSet["studentlname"].ToString(),
                            StudentNumber = resultSet["studentnumber"].ToString(),
                            EnrollDate = Convert.ToDateTime(resultSet["enroldate"])
                        };

                        // Return the student
                        return Ok(student);
                    }
                    else
                    {
                        // If the student is not found, return a 404 Not Found
                        return NotFound(new { message = "Student not found." });
                    }
                }
            }
        }

    }
}
