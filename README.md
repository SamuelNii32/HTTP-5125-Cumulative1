# HTTP-5125-Cumulative1

This repository contains the code for the HTTP-5125 Cumulative 1 assignment. The project implements a basic API that manages data for teachers and students within a simple educational system. The following features are included:

## Features:
- **Teacher Management:** 
  - List all teachers.
  - Retrieve teacher details by ID.
  - Filter teachers based on their hire date.
  - List all courses assigned to a teacher by their ID.

- **Student Management:** 
  - List all students.
  - Retrieve student details by ID.

## Endpoints:
- `GET /api/TeacherAPI/ListTeachers`: Lists all teachers in the system.
- `GET /api/TeacherAPI/FindTeacher/{id}`: Fetches details of a specific teacher based on their ID.
- `GET /api/TeacherAPI/ListCoursesByTeacherId/{id}`: Lists all courses assigned to a teacher based on their ID.
- `GET /api/StudentAPI/ListStudents`: Lists all students in the system.
- `GET /api/StudentAPI/GetStudentById/{id}`: Fetches details of a specific student based on their ID.

## Technologies Used:
- ASP.NET Core Web API
- C#
- Entity Framework Core (for database interaction)

## Setup:
1. Clone the repository to your local machine.
2. Open the solution in Visual Studio.
3. Run the application and access the endpoints using a tool like Postman or via browser (for GET requests).

## Additional Information:
This assignment demonstrates the implementation of an API for managing simple educational records. It includes routes for interacting with teacher and student data, and the ability to filter and retrieve relevant information.

Feel free to contribute or improve upon the code for further enhancements.
