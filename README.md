# 🎓 InnovateEd Hub: Student & Grade Tracker

A lightweight, professional web application designed for educators to manage student registrations and track academic performance across various courses. Built with **ASP.NET Core**, **Entity Framework Core**, and a modern **Glassmorphism UI**.

---

## ✨ Features

* **Global Error Handling:** Custom middleware catches and formats API errors into clean JSON responses.
* **Student Management:** Full CRUD (Create, Read, Update, Delete) capabilities for student profiles.
* **Academic Grading:** Assign scores to specific subjects (Mathematics, Science, History, etc.).
* **Live GPA Calculation:** The backend automatically calculates average scores using LINQ.
* **Interactive Dashboard:** A two-column interface allowing for simultaneous viewing of the student list and detailed grade history.
* **Persistence:** Powered by **SQLite** for a portable, zero-config database experience.

---

## 🛠️ Tech Stack

* **Backend:** .NET 10 / ASP.NET Core Web API
* **ORM:** Entity Framework Core
* **Database:** SQLite
* **Frontend:** Vanilla JavaScript (ES6+), HTML5, CSS3 (Custom Properties & Backdrop Filters)
* **API Documentation:** Swagger UI

---

## 🚀 Installation & Setup

### 1. Prerequisites
* [.NET 10.0 SDK](https://dotnet.microsoft.com/download)
* Visual Studio Code

### 2. Run the Application
1.  Open your terminal in the project root folder.
2.  Restore dependencies and run the project:
    ```bash
    dotnet run
    ```
3.  The application will automatically create the `students.db` file on its first run.

### 3. Accessing the App
* **Main Interface:** `http://localhost:5000/index.html` (Check your console for the exact port).
* **API Documentation:** `http://localhost:5000/swagger`

---

## 📁 Project Architecture

```text
├── Controllers/
│   └── StudentController.cs   # API Endpoints for Students and Grades
├── Middleware/
│   └── ExceptionMiddleware.cs # The "Safety Net" for global error catching
├── Models/
│   ├── Student.cs             # Student Entity (Name, Email, Grades List)
│   └── Grade.cs               # Grade Entity (Subject, Score, StudentId)
├── Data/
│   └── StudentContext.cs      # Database context and table definitions
└── wwwroot/
    ├── index.html             # Dashboard structure
    ├── style.css              # Glassmorphism styling
    └── app.js                 # API communication and UI logic
