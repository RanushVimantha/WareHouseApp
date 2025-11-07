# WareHouse Application - Use Case Diagram

```plantuml
@startuml WareHouseApp_UseCaseDiagram

left to right direction
skinparam packageStyle rectangle
skinparam actorStyle awesome

' Actors
actor "Admin User" as Admin
actor "Database System" as DB

' System boundary
rectangle "WareHouse Management System" {
    
    ' Authentication Use Cases
    package "Authentication" {
        usecase "Login" as UC1
        usecase "Sign Up" as UC2
        usecase "Logout" as UC3
        usecase "Change Password" as UC4
    }
    
    ' Customer Management Use Cases
    package "Customer Management" {
        usecase "View All Customers" as UC5
        usecase "Add Customer" as UC6
        usecase "Update Customer" as UC7
        usecase "Delete Customer" as UC8
        usecase "Search Customer" as UC9
    }
    
    ' Employee Management Use Cases
    package "Employee Management" {
        usecase "View All Employees" as UC10
        usecase "Add Employee" as UC11
        usecase "Update Employee" as UC12
        usecase "Delete Employee" as UC13
        usecase "Search Employee" as UC14
    }
    
    ' Material Management Use Cases
    package "Material Management" {
        usecase "View All Materials" as UC15
        usecase "Add Material" as UC16
        usecase "Update Material" as UC17
        usecase "Delete Material" as UC18
        usecase "View Inventory Value" as UC19
        usecase "Check Low Stock" as UC20
    }
    
    ' Dashboard Use Cases
    package "Dashboard & Analytics" {
        usecase "View Dashboard" as UC21
        usecase "View Statistics" as UC22
        usecase "View System Status" as UC23
    }
}

' Admin relationships
Admin --> UC1
Admin --> UC2
Admin --> UC3
Admin --> UC4

Admin --> UC5
Admin --> UC6
Admin --> UC7
Admin --> UC8
Admin --> UC9

Admin --> UC10
Admin --> UC11
Admin --> UC12
Admin --> UC13
Admin --> UC14

Admin --> UC15
Admin --> UC16
Admin --> UC17
Admin --> UC18
Admin --> UC19
Admin --> UC20

Admin --> UC21
Admin --> UC22
Admin --> UC23

' Include relationships
UC1 .> UC21 : <<include>>
UC21 .> UC22 : <<include>>
UC22 .> UC19 : <<include>>
UC22 .> UC20 : <<include>>

UC6 .> UC5 : <<include>>
UC7 .> UC5 : <<include>>
UC8 .> UC5 : <<include>>

UC11 .> UC10 : <<include>>
UC12 .> UC10 : <<include>>
UC13 .> UC10 : <<include>>

UC16 .> UC15 : <<include>>
UC17 .> UC15 : <<include>>
UC18 .> UC15 : <<include>>

' Extend relationships
UC9 ..> UC5 : <<extend>>
UC14 ..> UC10 : <<extend>>

' Database interactions
UC1 --> DB
UC2 --> DB
UC4 --> DB
UC5 --> DB
UC6 --> DB
UC7 --> DB
UC8 --> DB
UC9 --> DB
UC10 --> DB
UC11 --> DB
UC12 --> DB
UC13 --> DB
UC14 --> DB
UC15 --> DB
UC16 --> DB
UC17 --> DB
UC18 --> DB
UC19 --> DB
UC20 --> DB
UC22 --> DB

@enduml
```

## Description

This use case diagram illustrates all the functional requirements of the WareHouse Management System:

### Actors
1. **Admin User**: The primary user who manages the warehouse system
2. **Database System**: External system storing all data

### Use Case Packages

#### 1. Authentication
- Login to the system
- Sign up for new account
- Logout from the system
- Change password

#### 2. Customer Management
- View all customers in a grid
- Add new customer records
- Update existing customer information
- Delete customer records
- Search for specific customers

#### 3. Employee Management
- View all employees in a grid
- Add new employee records
- Update existing employee information
- Delete employee records
- Search for specific employees

#### 4. Material Management
- View all materials/inventory
- Add new materials
- Update material information (quantity, price)
- Delete materials
- View total inventory value
- Check for low stock items

#### 5. Dashboard & Analytics
- View main dashboard
- View statistics (material count, customer count, employee count)
- View system status and operational information

### Relationships
- **Include**: Mandatory relationships (e.g., Login includes navigating to Dashboard)
- **Extend**: Optional extensions (e.g., Search extends View operations)
- All CRUD operations interact with the Database System

