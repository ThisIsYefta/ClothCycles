using System;

namespace ClothCycles
{
    internal class Account
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual bool Login(string username, string password)
        {
            return username == this.Username && password == this.Password;
        }

        public virtual bool Register(string username, string email, string password)
        {
            this.Username = username;
            this.Email = email;
            this.Password = password;
            return true;
        }
    }
}
