USE master;
GO

IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'WarehouseDB')
BEGIN
    CREATE DATABASE WarehouseDB;
END
GO

USE WarehouseDB;
GO

-- Create tables
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Users')
BEGIN
    CREATE TABLE Users (
        UserID INT PRIMARY KEY IDENTITY(1,1),
        UserName NVARCHAR(100) NOT NULL UNIQUE,
        [Password] NVARCHAR(100) NOT NULL,
        [Role] NVARCHAR(50) NOT NULL CHECK ([Role] IN ('Admin', 'Operator')),
        CreatedDate DATETIME DEFAULT GETDATE()
    );
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Materials')
BEGIN
    CREATE TABLE Materials (
        MaterialID INT PRIMARY KEY IDENTITY(1,1),
        MaterialName NVARCHAR(200) NOT NULL,
        Quantity INT NOT NULL DEFAULT 0,
        Price DECIMAL(18,2) NOT NULL,
        [Description] NVARCHAR(500),
        CreatedDate DATETIME DEFAULT GETDATE(),
        ModifiedDate DATETIME DEFAULT GETDATE()
    );
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Customers')
BEGIN
    CREATE TABLE Customers (
        CustomerID INT PRIMARY KEY IDENTITY(1,1),
        FirstName NVARCHAR(100) NOT NULL,
        LastName NVARCHAR(100) NOT NULL,
        Email NVARCHAR(150),
        Phone NVARCHAR(20),
        [Address] NVARCHAR(300),
        CreatedDate DATETIME DEFAULT GETDATE(),
        ModifiedDate DATETIME DEFAULT GETDATE()
    );
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Employees')
BEGIN
    CREATE TABLE Employees (
        EmployeeID INT PRIMARY KEY IDENTITY(1,1),
        FirstName NVARCHAR(100) NOT NULL,
        LastName NVARCHAR(100) NOT NULL,
        [Role] NVARCHAR(100),
        Email NVARCHAR(150),
        Phone NVARCHAR(20),
        [Address] NVARCHAR(300),
        UserID INT NULL,
        CreatedDate DATETIME DEFAULT GETDATE(),
        ModifiedDate DATETIME DEFAULT GETDATE(),
        FOREIGN KEY (UserID) REFERENCES Users(UserID) ON DELETE SET NULL
    );
END
GO

-- Insert sample data
IF NOT EXISTS (SELECT * FROM Users WHERE UserName = 'admin')
BEGIN
    INSERT INTO Users (UserName, [Password], [Role])
    VALUES ('admin', 'admin123', 'Admin');
END
GO

IF NOT EXISTS (SELECT * FROM Materials)
BEGIN
    INSERT INTO Materials (MaterialName, Quantity, Price, [Description])
    VALUES 
        ('Steel Rods', 500, 25.50, 'High-grade steel rods for construction'),
        ('Cement Bags', 1000, 8.75, '50kg cement bags'),
        ('Plywood Sheets', 250, 45.00, '4x8 plywood sheets'),
        ('PVC Pipes', 300, 12.25, 'Standard PVC pipes'),
        ('Electric Cables', 750, 3.50, 'Copper electric cables'),
        ('Paint Cans', 200, 35.99, 'Interior wall paint'),
        ('Bricks', 5000, 0.75, 'Red clay bricks'),
        ('Sand (cubic meter)', 50, 45.00, 'Construction-grade sand'),
        ('Nails (box)', 150, 8.50, 'Assorted construction nails'),
        ('Screws (box)', 200, 12.75, 'Assorted screws');
END
GO

IF NOT EXISTS (SELECT * FROM Customers)
BEGIN
    INSERT INTO Customers (FirstName, LastName, Email, Phone, [Address])
    VALUES 
        ('John', 'Smith', 'john.smith@email.com', '555-0101', '123 Main St, Springfield'),
        ('Sarah', 'Johnson', 'sarah.j@email.com', '555-0102', '456 Oak Ave, Riverside'),
        ('Michael', 'Brown', 'mbrown@email.com', '555-0103', '789 Pine Rd, Lakeside'),
        ('Emily', 'Davis', 'emily.davis@email.com', '555-0104', '321 Elm St, Hilltown'),
        ('David', 'Wilson', 'dwilson@email.com', '555-0105', '654 Maple Dr, Greenfield'),
        ('Lisa', 'Anderson', 'lisa.a@email.com', '555-0106', '987 Cedar Ln, Brookville'),
        ('James', 'Taylor', 'jtaylor@email.com', '555-0107', '147 Birch Way, Sunnyside'),
        ('Jennifer', 'Thomas', 'jen.thomas@email.com', '555-0108', '258 Walnut St, Fairview'),
        ('Robert', 'Martinez', 'rmartinez@email.com', '555-0109', '369 Cherry Blvd, Parktown'),
        ('Maria', 'Garcia', 'mgarcia@email.com', '555-0110', '741 Spruce Ave, Valleyview');
END
GO

IF NOT EXISTS (SELECT * FROM Employees)
BEGIN
    INSERT INTO Employees (FirstName, LastName, [Role], Email, Phone, [Address], UserID)
    VALUES 
        ('Tom', 'Henderson', 'Manager', 'tom@warehouse.com', '555-1003', '111 Industrial Pkwy', NULL),
        ('Anna', 'Clark', 'Clerk', 'anna@warehouse.com', '555-1004', '222 Commerce St', NULL),
        ('Chris', 'Lee', 'Supervisor', 'chris@warehouse.com', '555-1005', '333 Trade Ave', NULL);
END
GO

-- ================================================
-- Create Indexes for Performance
-- ================================================
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Materials_MaterialName')
BEGIN
    CREATE NONCLUSTERED INDEX IX_Materials_MaterialName ON Materials(MaterialName);
    PRINT 'Index IX_Materials_MaterialName created successfully.';
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Customers_Email')
BEGIN
    CREATE NONCLUSTERED INDEX IX_Customers_Email ON Customers(Email);
    PRINT 'Index IX_Customers_Email created successfully.';
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Employees_Email')
BEGIN
    CREATE NONCLUSTERED INDEX IX_Employees_Email ON Employees(Email);
    PRINT 'Index IX_Employees_Email created successfully.';
END
GO

-- ================================================
-- Display Summary
-- ================================================
PRINT '';
PRINT '===========================================';
PRINT 'Database Setup Complete!';
PRINT '===========================================';
PRINT '';
PRINT 'Summary:';
SELECT 'Users' AS [Table], COUNT(*) AS [Row Count] FROM Users
UNION ALL
SELECT 'Materials', COUNT(*) FROM Materials
UNION ALL
SELECT 'Customers', COUNT(*) FROM Customers
UNION ALL
SELECT 'Employees', COUNT(*) FROM Employees;
PRINT '';
PRINT 'You can now run the WareHouse application!';
PRINT '';
PRINT '===========================================';
GO

