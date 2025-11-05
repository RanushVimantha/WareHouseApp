Warehouse Management System
===========================

This is my coursework for CS107.3 Object Oriented Programming with C#.

The application is a Windows Forms desktop application that manages warehouse inventory,
customers, and employees.


HOW TO RUN THE APPLICATION
---------------------------
1. Open the database.sql file in SQL Server and execute it
2. Open WareHouseApp.csproj in Visual Studio
3. Press F5 to run

Login Credentials: admin / admin123


FEATURES IMPLEMENTED
--------------------
- Sign Up & Login (with validation)
- Material/Inventory Management (Full CRUD)
- Customer Management (Full CRUD)
- Employee Management (Full CRUD)
- Dashboard with Navigation


OOP CONCEPTS DEMONSTRATED
-------------------------
- Encapsulation: Properties with get/set, private fields
- Inheritance: Person (abstract) -> Admin
- Polymorphism: Abstract method overriding (Login, ChangePassword, Logout)
- Abstraction: Abstract Person class
- Design Patterns: Repository Pattern for data access


TECHNICAL IMPLEMENTATION
------------------------
- Exception Handling: Try-catch blocks throughout application
- Database Integration: SQL Server with ADO.NET
- Input Validation: All forms validate user input
- Security: Parameterized queries to prevent SQL injection
- CRUD Operations: Create, Read, Update, Delete for all entities


VERSION CONTROL
---------------
Git is used for version control, managed through GitHub Desktop.

To set up version control:
1. Read: GITHUB_DESKTOP_GUIDE.txt (complete step-by-step instructions)
2. Install GitHub Desktop from: https://desktop.github.com/
3. Follow the 15 manual commit steps in the guide

Documentation:
- VERSION_CONTROL.md: Comprehensive version control documentation
- GITHUB_DESKTOP_GUIDE.txt: Step-by-step commit instructions


PROJECT STRUCTURE
-----------------
- Data/           : Database access layer (DataAccess class)
- Entities/       : Customer and Employee entity classes
- Material/       : Material entity class
- People/         : Abstract Person class and Admin implementation
- Repositories/   : Repository pattern implementations (CRUD operations)
- Resources/      : Images and application resources
- Properties/     : Assembly info and settings


DOCUMENTATION FILES
-------------------
- README.txt                : This file - project overview
- VERSION_CONTROL.md        : Git and version control documentation
- GITHUB_DESKTOP_GUIDE.txt  : Manual commit instructions (15 steps)
- How to Run.txt            : Setup instructions
- SUBMISSION_CHECKLIST.txt  : Items to verify before submission
- database.sql              : Complete database schema and sample data


DEVELOPED BY
------------
Student Name: [Your Name]
Module: CS107.3 - Object Oriented Programming with C#
Institution: NSBM Green University
Year: 2025
