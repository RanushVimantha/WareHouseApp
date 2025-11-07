using System;
using System.Data.SqlClient;
using WareHouseApp.Data;

namespace WareHouseApp.People
{
    // Operator class for users with limited permissions
    // Operators can view materials and customers but can't manage employees
    public class Operator : Person
    {
        private readonly DataAccess _data;

        public Operator()
        {
            _data = new DataAccess();
        }

        public override bool Login(string username, string password)
        {
            // Query to check if user is an Operator
            string sql = "SELECT UserID, [Password] FROM Users WHERE UserName = @user AND Role = 'Operator'";
            var table = _data.ExecuteQuery(sql,
                new SqlParameter("@user", username));

            if (table.Rows.Count == 1)
            {
                string dbPassword = table.Rows[0]["Password"].ToString();
                Id = Convert.ToInt32(table.Rows[0]["UserID"]);
                UserName = username;
                Password = dbPassword;

                // TODO: implement proper password hashing for security
                return (dbPassword == password);
            }
            return false;
        }

        public override void ChangePassword(string newPassword)
        {
            if (Id == 0) throw new InvalidOperationException("Not logged in.");
            
            // Update password in database
            string sql = "UPDATE Users SET [Password] = @pwd WHERE UserID = @id";
            _data.ExecuteNonQuery(sql,
                new SqlParameter("@pwd", newPassword),
                new SqlParameter("@id", Id));
            Password = newPassword;
        }

        public override void Logout()
        {
            // Clear session data
            Id = 0;
            UserName = null;
            Password = null;
        }
    }
}

