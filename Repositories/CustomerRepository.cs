using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WareHouseApp.Data;
using WareHouseApp.Entities;

namespace WareHouseApp.Repositories
{
    public class CustomerRepository
    {
        private readonly DataAccess _data;

        public CustomerRepository()
        {
            _data = new DataAccess();
        }

        public IEnumerable<Customer> GetAll()
        {
            var list = new List<Customer>();
            var table = _data.ExecuteQuery("SELECT * FROM Customers");
            foreach (DataRow row in table.Rows)
            {
                list.Add(new Customer
                {
                    CustomerID = (int)row["CustomerID"],
                    FirstName = row["FirstName"].ToString(),
                    LastName = row["LastName"].ToString(),
                    Email = row["Email"].ToString(),
                    Phone = row["Phone"].ToString(),
                    Address = row["Address"].ToString()
                });
            }
            return list;
        }

        public Customer GetById(int customerId)
        {
            string sql = "SELECT * FROM Customers WHERE CustomerID = @id";
            var table = _data.ExecuteQuery(sql, new SqlParameter("@id", customerId));
            if (table.Rows.Count == 0) return null;

            DataRow row = table.Rows[0];
            return new Customer
            {
                CustomerID = (int)row["CustomerID"],
                FirstName = row["FirstName"].ToString(),
                LastName = row["LastName"].ToString(),
                Email = row["Email"].ToString(),
                Phone = row["Phone"].ToString(),
                Address = row["Address"].ToString()
            };
        }

        public void Add(Customer customer)
        {
            string sql = "INSERT INTO Customers (FirstName, LastName, Email, Phone, Address) " +
                        "VALUES (@fname, @lname, @email, @phone, @address)";
            _data.ExecuteNonQuery(sql,
                new SqlParameter("@fname", customer.FirstName),
                new SqlParameter("@lname", customer.LastName),
                new SqlParameter("@email", customer.Email ?? (object)DBNull.Value),
                new SqlParameter("@phone", customer.Phone ?? (object)DBNull.Value),
                new SqlParameter("@address", customer.Address ?? (object)DBNull.Value));
        }

        public void Update(Customer customer)
        {
            string sql = "UPDATE Customers SET FirstName=@fname, LastName=@lname, Email=@email, " +
                        "Phone=@phone, Address=@address WHERE CustomerID=@id";
            _data.ExecuteNonQuery(sql,
                new SqlParameter("@fname", customer.FirstName),
                new SqlParameter("@lname", customer.LastName),
                new SqlParameter("@email", customer.Email ?? (object)DBNull.Value),
                new SqlParameter("@phone", customer.Phone ?? (object)DBNull.Value),
                new SqlParameter("@address", customer.Address ?? (object)DBNull.Value),
                new SqlParameter("@id", customer.CustomerID));
        }

        public void Delete(int customerId)
        {
            string sql = "DELETE FROM Customers WHERE CustomerID = @id";
            _data.ExecuteNonQuery(sql, new SqlParameter("@id", customerId));
        }

        public IEnumerable<Customer> Search(string searchText)
        {
            string sql = "SELECT * FROM Customers WHERE FirstName LIKE @search OR LastName LIKE @search OR Email LIKE @search";
            var list = new List<Customer>();
            var table = _data.ExecuteQuery(sql, new SqlParameter("@search", "%" + searchText + "%"));
            foreach (DataRow row in table.Rows)
            {
                list.Add(new Customer
                {
                    CustomerID = (int)row["CustomerID"],
                    FirstName = row["FirstName"].ToString(),
                    LastName = row["LastName"].ToString(),
                    Email = row["Email"].ToString(),
                    Phone = row["Phone"].ToString(),
                    Address = row["Address"].ToString()
                });
            }
            return list;
        }
    }
}
