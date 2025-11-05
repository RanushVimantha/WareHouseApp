using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WareHouseApp.Data;
using WareHouseApp.Entities;

namespace WareHouseApp.Repositories
{
    public class EmployeeRepository
    {
        private readonly DataAccess _data;

        public EmployeeRepository()
        {
            _data = new DataAccess();
        }

        public IEnumerable<Employee> GetAll()
        {
            var list = new List<Employee>();
            var table = _data.ExecuteQuery("SELECT * FROM Employees");
            foreach (DataRow row in table.Rows)
            {
                list.Add(new Employee
                {
                    EmployeeID = (int)row["EmployeeID"],
                    FirstName = row["FirstName"].ToString(),
                    LastName = row["LastName"].ToString(),
                    Role = row["Role"].ToString(),
                    Email = row["Email"].ToString(),
                    Phone = row["Phone"].ToString(),
                    Address = row["Address"].ToString(),
                    UserID = row["UserID"] != DBNull.Value ? (int)row["UserID"] : 0
                });
            }
            return list;
        }

        public Employee GetById(int employeeId)
        {
            string sql = "SELECT * FROM Employees WHERE EmployeeID = @id";
            var table = _data.ExecuteQuery(sql, new SqlParameter("@id", employeeId));
            if (table.Rows.Count == 0) return null;

            DataRow row = table.Rows[0];
            return new Employee
            {
                EmployeeID = (int)row["EmployeeID"],
                FirstName = row["FirstName"].ToString(),
                LastName = row["LastName"].ToString(),
                Role = row["Role"].ToString(),
                Email = row["Email"].ToString(),
                Phone = row["Phone"].ToString(),
                Address = row["Address"].ToString(),
                UserID = row["UserID"] != DBNull.Value ? (int)row["UserID"] : 0
            };
        }

        public void Add(Employee employee)
        {
            string sql = "INSERT INTO Employees (FirstName, LastName, Role, Email, Phone, Address, UserID) " +
                        "VALUES (@fname, @lname, @role, @email, @phone, @address, @userid)";
            _data.ExecuteNonQuery(sql,
                new SqlParameter("@fname", employee.FirstName),
                new SqlParameter("@lname", employee.LastName),
                new SqlParameter("@role", employee.Role ?? (object)DBNull.Value),
                new SqlParameter("@email", employee.Email ?? (object)DBNull.Value),
                new SqlParameter("@phone", employee.Phone ?? (object)DBNull.Value),
                new SqlParameter("@address", employee.Address ?? (object)DBNull.Value),
                new SqlParameter("@userid", employee.UserID > 0 ? (object)employee.UserID : DBNull.Value));
        }

        public void Update(Employee employee)
        {
            string sql = "UPDATE Employees SET FirstName=@fname, LastName=@lname, Role=@role, " +
                        "Email=@email, Phone=@phone, Address=@address, UserID=@userid WHERE EmployeeID=@id";
            _data.ExecuteNonQuery(sql,
                new SqlParameter("@fname", employee.FirstName),
                new SqlParameter("@lname", employee.LastName),
                new SqlParameter("@role", employee.Role ?? (object)DBNull.Value),
                new SqlParameter("@email", employee.Email ?? (object)DBNull.Value),
                new SqlParameter("@phone", employee.Phone ?? (object)DBNull.Value),
                new SqlParameter("@address", employee.Address ?? (object)DBNull.Value),
                new SqlParameter("@userid", employee.UserID > 0 ? (object)employee.UserID : DBNull.Value),
                new SqlParameter("@id", employee.EmployeeID));
        }

        public void Delete(int employeeId)
        {
            string sql = "DELETE FROM Employees WHERE EmployeeID = @id";
            _data.ExecuteNonQuery(sql, new SqlParameter("@id", employeeId));
        }

        public IEnumerable<Employee> Search(string searchText)
        {
            string sql = "SELECT * FROM Employees WHERE FirstName LIKE @search OR LastName LIKE @search OR Role LIKE @search OR Email LIKE @search";
            var list = new List<Employee>();
            var table = _data.ExecuteQuery(sql, new SqlParameter("@search", "%" + searchText + "%"));
            foreach (DataRow row in table.Rows)
            {
                list.Add(new Employee
                {
                    EmployeeID = (int)row["EmployeeID"],
                    FirstName = row["FirstName"].ToString(),
                    LastName = row["LastName"].ToString(),
                    Role = row["Role"].ToString(),
                    Email = row["Email"].ToString(),
                    Phone = row["Phone"].ToString(),
                    Address = row["Address"].ToString(),
                    UserID = row["UserID"] != DBNull.Value ? (int)row["UserID"] : 0
                });
            }
            return list;
        }
    }
}
