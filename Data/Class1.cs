using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WareHouseApp.Data
{
    public class DataAccess
    {
        private readonly string _connectionString;

        public DataAccess()
        {
            // Read connection string from App.config
            _connectionString = ConfigurationManager.ConnectionStrings["WarehouseDB"].ConnectionString;
        }

        // Execute a non‑query (INSERT, UPDATE, DELETE)
        public int ExecuteNonQuery(string query, params SqlParameter[] parameters)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        // Execute a scalar query (returns a single value)
        public object ExecuteScalar(string query, params SqlParameter[] parameters)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteScalar();
                }
            }
        }

        // Execute a reader query (SELECT) and return a DataTable
        public DataTable ExecuteQuery(string query, params SqlParameter[] parameters)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddRange(parameters);
                    using (var reader = cmd.ExecuteReader())
                    {
                        var table = new DataTable();
                        table.Load(reader);
                        return table;
                    }
                }
            }
        }
    }
}
