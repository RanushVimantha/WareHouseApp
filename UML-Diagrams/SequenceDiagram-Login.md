# WareHouse Application - Sequence Diagram (Login Flow)

```plantuml
@startuml WareHouseApp_LoginSequence

skinparam sequenceMessageAlign center
skinparam responseMessageBelowArrow true

actor "Admin User" as User
participant "Form1\n(Login Form)" as LoginForm
participant "Admin" as AdminObj
participant "DataAccess" as DataAccess
database "SQL Server\n(WarehouseDB)" as Database
participant "DashBoard\n(Main Form)" as Dashboard

autonumber

User -> LoginForm: Enter username & password
User -> LoginForm: Click "Login" button
activate LoginForm

LoginForm -> LoginForm: Validate input fields
note right
  - Check username not empty
  - Check password not empty
end note

alt Invalid Input
    LoginForm -> User: Show validation error message
    deactivate LoginForm
else Valid Input
    LoginForm -> AdminObj: Login(username, password)
    activate AdminObj
    
    AdminObj -> DataAccess: ExecuteQuery(sql, parameters)
    activate DataAccess
    note right
      SQL: SELECT UserID, [Password] 
      FROM Users 
      WHERE UserName = @user 
      AND Role = 'Admin'
    end note
    
    DataAccess -> Database: Open connection & execute query
    activate Database
    
    Database --> DataAccess: Return DataTable with results
    deactivate Database
    
    DataAccess --> AdminObj: Return DataTable
    deactivate DataAccess
    
    AdminObj -> AdminObj: Validate credentials
    note right
      - Check if exactly 1 row returned
      - Compare passwords
      - Set Id, UserName, Password fields
    end note
    
    alt Credentials Valid
        AdminObj --> LoginForm: Return true
        deactivate AdminObj
        
        LoginForm -> LoginForm: Hide login form
        
        LoginForm -> Dashboard: new DashBoard(userName)
        activate Dashboard
        
        Dashboard -> Dashboard: ConfigureDashboard()
        note right
          - Set up menu buttons
          - Style UI elements
        end note
        
        Dashboard -> Dashboard: ShowMainDash()
        Dashboard --> User: Display dashboard
        
        LoginForm -> User: Show dashboard form
        deactivate Dashboard
        deactivate LoginForm
        
    else Credentials Invalid
        AdminObj --> LoginForm: Return false
        deactivate AdminObj
        
        LoginForm -> User: Show "Invalid credentials" error
        LoginForm -> LoginForm: Clear password field
        deactivate LoginForm
    end
end

@enduml
```

## Description

This sequence diagram illustrates the complete login authentication flow in the WareHouse Application.

### Key Steps:

1. **User Input**: Admin enters username and password
2. **Validation**: Form1 validates input fields are not empty
3. **Authentication**: 
   - Admin object queries database through DataAccess
   - SQL query filters by username and 'Admin' role
   - Returns user record if exists
4. **Credential Verification**:
   - Checks exactly one matching record exists
   - Compares plain text passwords (note: not hashed in current implementation)
5. **Success Path**:
   - Login form hides itself
   - Creates and displays DashBoard form
   - Dashboard initializes with user's name
   - Shows main dashboard with statistics
6. **Failure Path**:
   - Returns error message to user
   - Clears password field for retry

### Design Patterns Observed:
- **Layered Architecture**: UI → Business Logic → Data Access → Database
- **Separation of Concerns**: Each layer has distinct responsibility
- **Error Handling**: Try-catch blocks at UI layer for exception handling

### Security Note:
The current implementation stores and compares plain text passwords. In a production system, this should use proper password hashing (bcrypt, PBKDF2, etc.).

