using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace TodoApi
{
    public sealed class Account
    {
        public string User { get; private set; }
        public string Password { get; private set; }
        public IEnumerable<Claim> Claims { get; private set; }
        public static readonly List<Account> Users = new List<Account>();
        public static IEnumerable<Account> UsersData => Users.AsReadOnly();

        public Account(string user, string password, List<Claim> claims)
        {
            if (user == null) throw new ArgumentNullException(nameof(user), "User cannot be null.");
            if (password == null) throw new ArgumentNullException(nameof(password), "Password cannot be null.");
            if (claims == null || claims.Count == 0) throw new ArgumentException("Claims cannot be null or empty.", nameof(claims));

            User = user;
            Password = password;
            Claims = claims;
            Users.Add(this);
        }

        public static void CreateMockUsers()
        {
            Users.Add(new Account("aaron@gmail.com", "123456", new List<Claim> { new Claim(ClaimTypes.Name, "aaron@gmail.com"), new Claim(ClaimTypes.Role, "Admin") }));
            Users.Add(new Account("aaron.chacon@gmal.com", "111111", new List<Claim> { new Claim(ClaimTypes.Name, "aaron.chacon@gmal.com"), new Claim(ClaimTypes.Role, "User") }));
            Users.Add(new Account("alonso.chacon@gmail.com", "222222", new List<Claim> { new Claim(ClaimTypes.Name, "alonso.chacon@gmail.com"), new Claim(ClaimTypes.Role, "Admin") }));
            Users.Add(new Account("chacon.aaron@gmail.com", "333333", new List<Claim> { new Claim(ClaimTypes.Name, "chacon.aaron@gmail.com"), new Claim(ClaimTypes.Role, "Admin") }));
        }
    }
}