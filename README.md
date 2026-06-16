# Employee Management System

## Description

Employee Management System is a web application that allows users to manage employee records efficiently. The application supports creating, updating, deleting, viewing, and searching employees by name or department. Employees are associated with departments through a SQL Server database, and basic validation is applied to ensure data integrity.

---

## Features

### Employee Management

* Add a new employee
* Edit employee information
* Delete an employee
* View employees in a table
* Search employees by name
* Search employees by department

### Department Management

* Create and manage departments
* Link employees to departments

### Employee Information

Each employee contains:

* Employee ID
* Full Name
* Email
* Mobile Number
* Department
* Job Title
* Hire Date
* Is Active

---

## Technologies Used

### Backend

* ASP.NET Core Web API
* C#
* Entity Framework Core

### Frontend

* HTML
* CSS
* JavaScript
* Bootstrap

### Database

* SQL Server

### Version Control

* Git
* GitHub

---

## Project Structure

```text
EmployeeManagementSystem
│
├── Controllers
├── Models
├── Data
├── Repositories
├── Services
├── Views
│   ├── Employees
│   └── Departments
├── wwwroot
├── Migrations
└── Program.cs
```

---

## Database

The application uses SQL Server as the database.

### Tables

#### Departments

* DepartmentId
* DepartmentName

#### Employees

* EmployeeId
* FullName
* Email
* MobileNumber
* DepartmentId
* JobTitle
* HireDate
* IsActive

---

## Validation

Basic validation is implemented for:

* Required fields
* Email format
* Mobile number format
* Department selection

---

## Bonus Features

* Clean and responsive UI using Bootstrap
* Good database structure
* Proper error handling
* Entity Framework Core migrations
* Search functionality
* Pagination and sorting

---

## How to Run the Project

### 1. Clone the Repository

```bash
git clone https://github.com/your-username/EmployeeManagementSystem.git
```

### 2. Open the Solution

Open the project in Visual Studio.

### 3. Configure SQL Server

Update the connection string in `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=EmployeeDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

### 4. Apply Migrations

```powershell
Update-Database
```

or

```powershell
dotnet ef database update
```

### 5. Run the Application

```powershell
dotnet run
```

The API will be available at:

```text
https://localhost:7163/index.html
```

Swagger UI:

```text
https://localhost:7163/swagger
```

---

## Screenshots

### Dashboard

Add screenshot here.

### Employee List

Add screenshot here.

### Add Employee Form

Add screenshot here.

### Department Management

Add screenshot here.

---

## Included Files

* Source code
* Entity Framework migrations
* Database script or migration files
* Screenshots
* README.md

---

## Future Improvements

* Authentication and authorization
* Repository and Service pattern
* Export data to Excel/PDF
* Advanced filtering
* Dashboard statistics
* Role-based access control

---

## Author

Developed using ASP.NET Core Web API, SQL Server, HTML, CSS, JavaScript, and Bootstrap.

