namespace NancyFxApp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Nancy.Authentication.Forms;
    using Nancy.Security;
    using Nancy;
    using System.Security.Cryptography;
    using System.Text;

    public class UserDatabase : IUserMapper
    {
        static UserDatabase()
        {
       }

        public IUserIdentity GetUserFromIdentifier(Guid identifier, NancyContext context)
        {
            var repository = Generics.Repository.getInstance();
            Models.User user = repository.find<Models.User>(identifier.ToString(), "guid");
            return user;
        }

        public static string getPasswordHash(string pass)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(pass);

            var sha1 = SHA1.Create();
            byte[] hashBytes = sha1.ComputeHash(bytes);

            var sb = new StringBuilder();
            foreach (byte b in hashBytes)
            {
                var hex = b.ToString("x2");
                sb.Append(hex);
            }
            return sb.ToString();
        }

        public static Models.User ValidateUser(string username, string password)
        {
            var repository = Generics.Repository.getInstance();
            Models.User user = repository.find<Models.User>(username, "login");
            if (user == null || user.Password != getPasswordHash(password))
            {
                return null;
            }

            return user;
        }
    }
}