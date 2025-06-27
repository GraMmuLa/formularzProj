using Project_PrzedmiotBranzowy_BackEnd.DAL;
using Project_PrzedmiotBranzowy_BackEnd.Models;
using Project_PrzedmiotBranzowy_Core.Helpers;
using System.Security.Cryptography;
using System.Text;

namespace Project_PrzedmiotBranzowy_Core.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly IApplicationDbContext _dbContext;

        public SecurityService(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public LoginCodes Login(string? username, string? password)
        {
            if (string.IsNullOrEmpty(username))
                return LoginCodes.EMPTY_USERNAME;
            if (string.IsNullOrEmpty(password))
                return LoginCodes.EMPTY_PASSWORD;

            Admin? foundAdmin = _dbContext.Admins.FirstOrDefault((x) => x.Username == username);
            
            if (foundAdmin is null)
                return LoginCodes.WRONG_USERNAME;
            
            password = EncryptPassword(password);
            if(password != foundAdmin.Password)
                return LoginCodes.WRONG_PASSWORD;
                
            return LoginCodes.OK;
        }

        public LoginCodes Register(string? username, string? password, string? repeatPassword)
        {
            if (string.IsNullOrEmpty(username))
                return LoginCodes.EMPTY_USERNAME;
            if (string.IsNullOrEmpty(password))
                return LoginCodes.EMPTY_PASSWORD;
            if (string.IsNullOrEmpty(repeatPassword))
                return LoginCodes.EMPTY_PASSWORD_REPEAT;
            if (repeatPassword != password)
                return LoginCodes.DIFFERENT_PASSWORDS;

            password = EncryptPassword(password);

            Admin newAdmin = new()
            {
                Username = username,
                Password = password,
            };

            _dbContext.Admins.Add(newAdmin);
            _dbContext.SaveChanges();

            return LoginCodes.OK;
        }

        private static string EncryptPassword(string password)
        {
            StringBuilder hash = new();

            byte[] hashArray = SHA256.HashData(Encoding.UTF8.GetBytes(password));

            foreach (byte b in hashArray)
                hash.Append(b.ToString("x"));

            return hash.ToString();
        }
    }
}
