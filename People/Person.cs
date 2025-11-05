using System;

namespace WareHouseApp.People
{
    public abstract class Person
    {
        // Protected fields encourage encapsulation
        protected int Id;
        protected string UserName;
        protected string Password;  // ideally hashed

        // Abstract methods enforce polymorphism across derived classes
        public abstract bool Login(string username, string password);
        public abstract void ChangePassword(string newPassword);
        public abstract void Logout();
    }
}
