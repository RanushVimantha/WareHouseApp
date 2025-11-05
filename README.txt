Warehouse Management System
===========================

CS107.3 - Object Oriented Programming with C#
NSBM Green University


DESCRIPTION
-----------
A Windows Forms desktop application that manages warehouse inventory,
customers, and employees with full CRUD operations.


HOW TO RUN
----------
1. Open database.sql in SQL Server and execute it
2. Open WareHouseApp.csproj in Visual Studio
3. Press F5 to run

Default Login: admin / admin123


FEATURES
--------
- Sign Up & Login with validation
- Material/Inventory Management (Create, Read, Update, Delete)
- Customer Management (Create, Read, Update, Delete)
- Employee Management (Create, Read, Update, Delete)
- Dashboard with navigation


OOP CONCEPTS DEMONSTRATED
-------------------------
- Encapsulation: Properties with get/set, private fields
- Inheritance: Person (abstract) -> Admin
- Polymorphism: Abstract method overriding
- Abstraction: Abstract Person class
- Design Patterns: Repository Pattern


TECHNICAL IMPLEMENTATION
------------------------
- Exception Handling: Try-catch blocks throughout
- Database Integration: SQL Server with ADO.NET
- Input Validation: All forms validate user input
- Security: Parameterized queries (SQL injection prevention)
- Version Control: Git (managed through GitHub Desktop)


PROJECT STRUCTURE
-----------------
Data/         - Database access layer
Entities/     - Customer and Employee classes
Material/     - Material entity class
People/       - Person (abstract) and Admin classes
Repositories/ - Repository pattern implementations
Resources/    - Images and UI resources
