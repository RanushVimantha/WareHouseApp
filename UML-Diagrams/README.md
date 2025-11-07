# WareHouse Application - UML Diagrams

This directory contains comprehensive UML diagrams for the WareHouse Management System application. The diagrams provide detailed documentation of the system's architecture, design, and behavior.

## 📋 Table of Contents

1. [Class Diagram](#class-diagram)
2. [Use Case Diagram](#use-case-diagram)
3. [Sequence Diagrams](#sequence-diagrams)
4. [Component Diagram](#component-diagram)
5. [Activity Diagram](#activity-diagram)
6. [Deployment Diagram](#deployment-diagram)
7. [State Diagram](#state-diagram)
8. [How to View](#how-to-view)
9. [Design Patterns](#design-patterns)
10. [Technology Stack](#technology-stack)

---

## 📦 Class Diagram

**File**: `ClassDiagram.md`

**Purpose**: Shows the static structure of the application, including all classes, their attributes, methods, and relationships.

**Key Components**:
- **Entities**: Customer, Employee, Material (POCOs)
- **People**: Person (abstract), Admin (authentication)
- **Data**: DataAccess (ADO.NET wrapper)
- **Repositories**: CustomerRepository, EmployeeRepository, MaterialRepository
- **UI Forms**: Form1, SignUpForm, DashBoard, Management UserControls

**Relationships**:
- Inheritance (Person → Admin)
- Composition (DashBoard contains UserControls)
- Association (Repositories use DataAccess)
- Dependency (UI uses Business Logic)

---

## 👤 Use Case Diagram

**File**: `UseCaseDiagram.md`

**Purpose**: Illustrates the functional requirements from the user's perspective.

**Actors**:
- **Admin User**: Primary user managing the warehouse
- **Database System**: External system for data persistence

**Use Case Packages**:
1. **Authentication**: Login, Sign Up, Logout, Change Password
2. **Customer Management**: CRUD operations + Search
3. **Employee Management**: CRUD operations + Search
4. **Material Management**: CRUD operations + Inventory analytics
5. **Dashboard & Analytics**: View statistics and system status

**Relationships**:
- Include (mandatory flows)
- Extend (optional extensions)

---

## 🔄 Sequence Diagrams

### Login Flow
**File**: `SequenceDiagram-Login.md`

**Purpose**: Details the authentication process from login to dashboard display.

**Flow**:
1. User enters credentials
2. Form validates input
3. Admin object queries database
4. Credentials verified
5. Dashboard displayed on success

**Error Handling**: Shows both success and failure paths.

### Customer CRUD Operations
**File**: `SequenceDiagram-CustomerCRUD.md`

**Purpose**: Demonstrates all CRUD operations for customer management.

**Operations Covered**:
1. **Load**: Retrieve all customers from database
2. **Add**: Create new customer record
3. **Update**: Modify existing customer
4. **Delete**: Remove customer (with confirmation)
5. **Search**: Filter customers by text

**Pattern**: Same flow applies to Employee and Material management.

---

## 🏗️ Component Diagram

**File**: `ComponentDiagram.md`

**Purpose**: Shows the high-level architecture and component relationships.

**Layers**:
1. **Presentation Layer**: Windows Forms UI
2. **Business Logic Layer**: Authentication + Repositories
3. **Data Access Layer**: DataAccess (ADO.NET)
4. **Domain Model**: Entity classes
5. **External Systems**: SQL Server database

**Design Patterns**:
- Layered Architecture
- Repository Pattern
- Singleton-like (DataAccess)
- Factory Pattern (Entity creation)

---

## 🔀 Activity Diagram

**File**: `ActivityDiagram-MaterialManagement.md`

**Purpose**: Illustrates the workflow for material management operations.

**Activities**:
1. Load materials
2. Add material (with validation)
3. Update material (with validation)
4. Delete material (with confirmation)
5. View inventory value
6. Check low stock alerts
7. Clear form

**Decision Points**:
- Validation success/failure
- User confirmation
- Low stock detection

**Parallel Paths**: Shows that operations are independent user choices.

---

## 🌐 Deployment Diagram

**File**: `DeploymentDiagram.md`

**Purpose**: Documents how the application is deployed in physical infrastructure.

**Nodes**:
1. **Client Workstation**: 
   - Windows OS
   - .NET Framework
   - Application executable
   - Configuration files

2. **Database Server**: 
   - SQL Server Express/LocalDB
   - Database files (MDF/LDF)

**Deployment Scenarios**:
1. **Standalone**: Single PC with LocalDB
2. **Multi-User**: Network with centralized SQL Server
3. **Hybrid**: Multiple locations with sync

**Network**: TCP/IP connection over port 1433 (SQL Server)

---

## 🔄 State Diagram

**File**: `StateDiagram-UserSession.md`

**Purpose**: Models the complete user session lifecycle.

**Major States**:
1. **Application Started**: Login screen
2. **Authenticated**: Logged in, can access all features
   - Showing Dashboard
   - Managing Customers
   - Managing Employees
   - Managing Materials
3. **Logging Out**: Confirmation dialog
4. **Logged Out**: Session cleanup, return to login

**Substates**:
- Each management module has CRUD substates
- Validation loops for Add/Edit operations
- Confirmation substates for Delete operations

**Transitions**:
- Guard conditions (e.g., valid credentials)
- Actions on transitions (e.g., clear form, refresh data)

---

## 👁️ How to View

### Option 1: PlantUML (Recommended)

1. **Install PlantUML**:
   - Download from: https://plantuml.com/download
   - Or use VS Code extension: "PlantUML" by jebbs

2. **View Diagrams**:
   - Open any `.md` file in VS Code
   - PlantUML extension will auto-render
   - Or use: `Ctrl+Shift+P` → "PlantUML: Preview Current Diagram"

3. **Export as Images**:
   - `Ctrl+Shift+P` → "PlantUML: Export Current Diagram"
   - Choose format: PNG, SVG, PDF

### Option 2: Online PlantUML Editor

1. Visit: http://www.plantuml.com/plantuml/uml/
2. Copy the PlantUML code from any `.md` file (between ` ```plantuml` tags)
3. Paste into the editor
4. View rendered diagram
5. Download as image if needed

### Option 3: IntelliJ IDEA / PyCharm

1. Install PlantUML Integration plugin
2. Open `.md` files
3. Click "View UML Diagram" button

### Option 4: Generate PNG Files

Using PlantUML CLI:
```bash
plantuml ClassDiagram.md -tpng
plantuml UseCaseDiagram.md -tpng
# ... repeat for all diagrams
```

---

## 🎨 Design Patterns

The WareHouse Application implements several design patterns:

### 1. **Repository Pattern**
**Where**: CustomerRepository, EmployeeRepository, MaterialRepository

**Purpose**: Abstracts data access logic from business logic

**Benefits**:
- Centralized data access
- Easy to test (mock repositories)
- Decouples UI from database

### 2. **Layered Architecture**
**Layers**: Presentation → Business Logic → Data Access → Database

**Purpose**: Separation of concerns

**Benefits**:
- Maintainability
- Testability
- Scalability

### 3. **Abstract Factory / Template Method**
**Where**: Person (abstract) → Admin (concrete)

**Purpose**: Define authentication contract

**Benefits**:
- Polymorphism
- Easy to extend (add more user types)

### 4. **Singleton-like Pattern**
**Where**: DataAccess with centralized connection string

**Purpose**: Single point of database access

**Benefits**:
- Consistent database access
- Easy configuration management

### 5. **Factory Pattern**
**Where**: Repositories create entity instances from DataTable

**Purpose**: Object creation from database results

**Benefits**:
- Encapsulates object creation logic
- Type-safe entity creation

### 6. **MVC/MVP Pattern**
**Where**: Separation of UI (Views), Business Logic (Controllers/Presenters), Data (Models)

**Purpose**: Separation of presentation and logic

**Benefits**:
- Clean code organization
- Easy UI changes without affecting logic

---

## 🛠️ Technology Stack

### Application Framework
- **Language**: C# 
- **Framework**: .NET Framework 4.7.2+
- **UI Framework**: Windows Forms (WinForms)

### Database
- **DBMS**: Microsoft SQL Server
- **Deployment Options**: 
  - SQL Server Express (network scenarios)
  - SQL Server LocalDB (standalone scenarios)
- **Database File**: WarehouseDB.mdf (attached database)

### Data Access
- **Technology**: ADO.NET
- **Approach**: Direct SQL queries with parameterization
- **Components**: 
  - SqlConnection
  - SqlCommand
  - SqlDataAdapter
  - DataTable

### Configuration
- **File**: App.config (XML)
- **Contains**: Connection strings, app settings

### Version Control
- **System**: Git
- **Hosting**: GitHub (implied by .gitignore patterns)

---

## 📊 Database Schema

### Tables

1. **Users**
   - UserID (PK, int, Identity)
   - UserName (nvarchar)
   - Password (nvarchar) - Plain text (should be hashed)
   - Role (nvarchar) - e.g., 'Admin'

2. **Customers**
   - CustomerID (PK, int, Identity)
   - FirstName (nvarchar)
   - LastName (nvarchar)
   - Email (nvarchar, nullable)
   - Phone (nvarchar, nullable)
   - Address (nvarchar, nullable)

3. **Employees**
   - EmployeeID (PK, int, Identity)
   - FirstName (nvarchar)
   - LastName (nvarchar)
   - Role (nvarchar, nullable)
   - Email (nvarchar, nullable)
   - Phone (nvarchar, nullable)
   - Address (nvarchar, nullable)
   - UserID (FK to Users, nullable)

4. **Materials**
   - MaterialID (PK, int, Identity)
   - MaterialName (nvarchar)
   - Quantity (int)
   - Price (decimal)
   - Description (nvarchar, nullable)

---

## 🔐 Security Considerations

### Current Implementation
- **Authentication**: Username/password (plain text)
- **Authorization**: Single role (Admin)
- **SQL Injection Prevention**: Parameterized queries ✅
- **Password Storage**: Plain text ⚠️

### Recommended Improvements
1. **Password Hashing**: Use bcrypt, PBKDF2, or Argon2
2. **Role-Based Access Control**: Multiple user roles
3. **Connection String Encryption**: Encrypt App.config
4. **SSL/TLS**: Encrypt database connections
5. **Audit Logging**: Track all CRUD operations
6. **Session Management**: Timeout inactive sessions

---

## 📈 Future Enhancements

### Architecture
1. **Web Application**: Migrate to ASP.NET Core
2. **REST API**: Add middle-tier API
3. **Microservices**: Split into independent services
4. **Cloud Deployment**: Azure App Service + Azure SQL

### Features
1. **Reporting**: Generate PDF/Excel reports
2. **Charts**: Visualize inventory trends
3. **Notifications**: Email alerts for low stock
4. **Barcode Scanning**: Mobile app for inventory
5. **Multi-Language**: Internationalization (i18n)

### Data
1. **Advanced Search**: Full-text search
2. **Data Export**: CSV, Excel, JSON export
3. **Data Import**: Bulk import from files
4. **Audit Trail**: Track all changes with timestamp

---

## 📝 Notes

1. **PlantUML Syntax**: All diagrams use PlantUML syntax for consistency and version control.

2. **Living Documentation**: These diagrams should be updated as the application evolves.

3. **Design Decisions**: Each diagram includes notes explaining key design decisions.

4. **Patterns**: Design patterns are documented within the diagrams for educational purposes.

5. **Security**: Security concerns are noted in relevant diagrams (especially Deployment and Sequence diagrams).

---

## 📞 Support

For questions about these diagrams or the application architecture:

1. Review the inline documentation in the source code
2. Check the `database.sql` file for database schema
3. Read the `How to Run.txt` for setup instructions
4. Examine the `README.txt` for application overview

---

## 📄 License

These diagrams are part of the WareHouse Application project and follow the same license as the main application.

---

**Generated**: November 2025  
**Application Version**: 1.0  
**UML Standard**: UML 2.5  
**Diagram Tool**: PlantUML

---

## Quick Reference

| Diagram Type | Focus | Best For Understanding |
|-------------|-------|----------------------|
| Class | Structure | Classes and relationships |
| Use Case | Functionality | What users can do |
| Sequence | Behavior | How operations work |
| Component | Architecture | System organization |
| Activity | Workflow | Business processes |
| Deployment | Infrastructure | How to deploy |
| State | Lifecycle | Session management |

---

*End of UML Diagrams Documentation*

