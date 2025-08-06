# Role-Based-Access-Control-RBAC-Web-Application
This project implements a Role-Based Access Control (RBAC) system using a .NET (C#) backend and a React frontend


Role-Based Access Control (RBAC) System
A full-stack RBAC web application implemented using .NET Core (C#) for the backend and React for the frontend. The application uses JWT Authentication and Role-Based Authorization to control access to APIs and UI components.

**Objective**
To design and implement an RBAC system where access to routes and APIs is controlled based on user roles:
Admin → Full access including user management.
Editor → Can create and edit content (no user management).
Viewer → Read-only access.

**Tech Stack**
Backend: .NET Core 8 (C#), Entity Framework Core (Code-First)
Frontend: React (JavaScript), Axios, React Router
Database: SQL Server
Authentication: JWT
Logging: Serilog
API Documentation: Swagger
Testing: xUnit (Backend Unit Testing)

**Prerequisites**
Node.js v16+
npm or yarn
.NET Core 6/7+
SQL Server
Visual Studio or VS Code
Swagger for API testing
Postman (optional)

**Packages Installed**
Frontend
npm install axios react-router-dom bootstrap
Backend
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
dotnet add package Swashbuckle.AspNetCore
dotnet add package Serilog.AspNetCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
Testing
dotnet add package xunit
dotnet add package xunit.runner.visualstudio
dotnet add package Moq

**Design Patterns Used**

✔ MVC Pattern

✔ Repository Pattern

✔ Dependency Injection

✔ Singleton Pattern (for logging)



**Features Implemented**

Roles & Permissions
Admin
  Full access to all APIs and routes
  Manage users and roles
Editor
  Can create and edit content
  Cannot manage users
Viewer
  Read-only access


**Logging**

Serilog for structured logging
Logs to:
Console
File (logs/log.txt)
Logs authentication, role updates, errors, and API calls

**Swagger**

API documentation available at:
https://localhost:7254/swagger

Database
EF Core Code-First approach
Run migrations:
dotnet ef migrations add InitialCreate
dotnet ef database update

 **How to Run**

Backend
cd [projectname].API
dotnet restore
dotnet build
dotnet run
API runs on: https://localhost:7254
Frontend
cd rbac-frontend
npm install
npm start
App runs on: http://localhost:3000

Example API Payload
Login

POST /api/v1/Auth/login
{
  "username": "admin",
  "password": "Admin@123"
}

Change Role

PUT /api/v1/UserManager/change-role
{
  "UserId": 2,
  "Role": "Editor"
}

**Backend (.NET Core)**

 JWT Authentication

 Role-based Authorization with Policies

Middleware for token & role validation

Repository + Service Pattern for clean architecture

APIs:

  POST /api/v1/Auth/login → Authenticate user
  
  POST /api/v1/Auth/register → Register user (with role)
  
  GET /api/v1/UserManager/all-users → Admin only
  
  PUT /api/v1/UserManager/change-role → Admin only
  
  GET /api/v1/Content/view → All roles
  
  POST /api/v1/Content/add → Admin & Editor only

<img width="1366" height="768" alt="image" src="https://github.com/user-attachments/assets/790fecef-01e3-4992-8333-890ec6e59603" />


**Frontend (React)**

✔ Login Page → Authenticates user and stores JWT in localStorage

✔ Role-Based Routing
Admin → Dashboard + User Management
Editor → Dashboard (Content edit only)
Viewer → Read-only dashboard

✔ Protected Routes using React Router

✔ Dynamic UI Rendering (hiding unauthorized UI components)

✔ User Management UI (Admin only)

Fetch all users
Change user roles dynamically

Role selection during registration

✔ Full User Management UI for Admin

✔ Logging with Serilog

✔ Swagger API documentation

✔ Unit Testing for backend using xUnit


**Unit Testing**
We implemented unit tests for backend logic using xUnit and Moq for mocking dependencies.

Key Areas Tested
✔ Authentication Service

Valid user login returns JWT token
Invalid login throws exception

✔ User Service
Get all users returns correct data
Change role updates user role successfully

✔ Authorization
Unauthorized role access throws 403 Forbidden
Admin can access restricted endpoints

Test Frameworks & Packages
Installed for backend:
dotnet add package xunit
dotnet add package xunit.runner.visualstudio
dotnet add package Moq
dotnet add package Microsoft.NET.Test.Sdk

Run Tests
cd RBAC.Tests
dotnet test

Sample UI Screenshots

<img width="1366" height="768" alt="image" src="https://github.com/user-attachments/assets/0036f91d-9737-45ac-a1da-f88539b72f18" />
<img width="1366" height="768" alt="image" src="https://github.com/user-attachments/assets/6f7905ad-82e5-46ce-bd1e-181ed17fb9d7" />
<img width="1366" height="768" alt="image" src="https://github.com/user-attachments/assets/895df775-76e4-4218-baab-851482eb0ee1" />
<img width="1366" height="768" alt="image" src="https://github.com/user-attachments/assets/1d161868-f213-47a3-88cb-7619321f98e4" />
<img width="1366" height="768" alt="image" src="https://github.com/user-attachments/assets/c2e0efaa-39e6-4746-9832-68c45e301008" />
<img width="1366" height="768" alt="image" src="https://github.com/user-attachments/assets/52c8e07d-996c-4a1d-855e-38e165f07408" />
<img width="1366" height="768" alt="image" src="https://github.com/user-attachments/assets/3f540745-f57a-434b-8e1a-c201fe540e45" />
<img width="1366" height="768" alt="image" src="https://github.com/user-attachments/assets/135f2bc1-cb3c-4d4d-9d84-8459d0fb574e" />











**Future Enhancements**
Password reset functionality
Search and filter users in UI









