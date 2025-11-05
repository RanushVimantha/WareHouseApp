using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WareHouseApp.Data;
using WareHouseApp.Materials;

namespace WareHouseApp.Repositories
{
    public class MaterialRepository
    {
        private readonly DataAccess _data;

        public MaterialRepository()
        {
            _data = new DataAccess();
        }

        public IEnumerable<Material> GetAll()
        {
            var list = new List<Material>();
            var table = _data.ExecuteQuery("SELECT * FROM Materials");
            foreach (DataRow row in table.Rows)
            {
                list.Add(new Material
                {
                    MaterialID = (int)row["MaterialID"],
                    MaterialName = row["MaterialName"].ToString(),
                    Quantity = (int)row["Quantity"],
                    Price = (decimal)row["Price"],
                    Description = row["Description"].ToString()
                });
            }
            return list;
        }

        public void Add(Material mat)
        {
            string sql = "INSERT INTO Materials (MaterialName, Quantity, Price, Description) VALUES (@name, @qty, @price, @desc)";
            _data.ExecuteNonQuery(sql,
                new SqlParameter("@name", mat.MaterialName),
                new SqlParameter("@qty", mat.Quantity),
                new SqlParameter("@price", mat.Price),
                new SqlParameter("@desc", mat.Description ?? (object)DBNull.Value));
        }

        public void Update(Material mat)
        {
            string sql = "UPDATE Materials SET MaterialName=@name, Quantity=@qty, Price=@price, Description=@desc WHERE MaterialID=@id";
            _data.ExecuteNonQuery(sql,
                new SqlParameter("@name", mat.MaterialName),
                new SqlParameter("@qty", mat.Quantity),
                new SqlParameter("@price", mat.Price),
                new SqlParameter("@desc", mat.Description ?? (object)DBNull.Value),
                new SqlParameter("@id", mat.MaterialID));
        }

        public void Delete(int materialId)
        {
            string sql = "DELETE FROM Materials WHERE MaterialID = @id";
            _data.ExecuteNonQuery(sql, new SqlParameter("@id", materialId));
        }
    }

    // Similar repositories can be built for Customers and Employees
}
