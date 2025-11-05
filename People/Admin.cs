using System;
using System.Data.SqlClient;
using WareHouseApp.Data;

namespace WareHouseApp.People
{
    public class Admin : Person
    {
        private readonly DataAccess _data;

        public Admin()
        {
            _data = new DataAccess();
        }

        public override bool Login(string username, string password)
        {
            string sql = "SELECT UserID, [Password] FROM Users WHERE UserName = @user AND Role = 'Admin'";
            var table = _data.ExecuteQuery(sql,
                new SqlParameter("@user", username));

            if (table.Rows.Count == 1)
            {
                string dbPassword = table.Rows[0]["Password"].ToString();
                Id = Convert.ToInt32(table.Rows[0]["UserID"]);
                UserName = username;
                Password = dbPassword;

                // NOTE: For real projects, compare hashed passwords
                return (dbPassword == password);
            }
            return false;
        }

        public override void ChangePassword(string newPassword)
        {
            if (Id == 0) throw new InvalidOperationException("Not logged in.");
            string sql = "UPDATE Users SET [Password] = @pwd WHERE UserID = @id";
            _data.ExecuteNonQuery(sql,
                new SqlParameter("@pwd", newPassword),
                new SqlParameter("@id", Id));
            Password = newPassword;
        }

        public override void Logout()
        {
            Id = 0;
            UserName = null;
            Password = null;
        }
    }
}
