HTTP-5125-Cumulative1
This is the repository for the assignment project for HTTP-5125 (Cumulative 1). The project includes a set of RESTful APIs and other functionalities designed to interact with a simple educational system.

Features
Teacher API:

List Teachers
Find Teacher by ID
List Courses by Teacher ID
List Teachers by Hire Date Range
Student API:

List Students
Get Student by ID
Installation
To run this project locally, follow these steps:

Clone the repository:

bash
Copy
Edit
git clone https://github.com/SamuelNii32/HTTP-5125-Cumulative1.git
Navigate to the project directory:

bash
Copy
Edit
cd HTTP-5125-Cumulative1
Open the solution file: You can open the solution file HTTP-5125-Cumulative1.sln in Visual Studio.

Restore dependencies: In Visual Studio, the dependencies should be restored automatically. If not, you can restore them manually via the NuGet Package Manager.

Run the project: Press F5 to start the application, or use Ctrl+F5 to run without debugging.

Usage
This project exposes several API endpoints. Here are the available API calls:

Teacher API
Get all teachers:

Method: GET
Endpoint: /api/TeacherAPI/ListTeachers
Get a teacher by ID:

Method: GET
Endpoint: /api/TeacherAPI/FindTeacher/{id}
Get courses by teacher ID:

Method: GET
Endpoint: /api/TeacherAPI/ListCoursesByTeacherId/{teacherId}
Get teachers by hire date range:

Method: GET
Endpoint: /api/TeacherAPI/ListTeachersByHireDateRange?startDate={startDate}&endDate={endDate}
Student API
Get all students:

Method: GET
Endpoint: /api/StudentAPI/ListStudents
Get student by ID:

Method: GET
Endpoint: /api/StudentAPI/GetStudentById/{id}
Technologies Used
C#
ASP.NET Core
Entity Framework
SQL Server
