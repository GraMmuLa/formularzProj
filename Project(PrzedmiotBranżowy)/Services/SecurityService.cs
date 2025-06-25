using Project_PrzedmiotBranżowy_BackEnd.DAL;
using Project_PrzedmiotBranżowy_BackEnd.Models;
using System.Security.Cryptography;
using System.Text;

namespace Project_PrzedmiotBranżowy_.Services
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
            if (username is null)
                return LoginCodes.EMPTY_USERNAME;
            if (password is null)
                return LoginCodes.EMPTY_PASSWORD;

            Admin? foundAdmin = _dbContext.Admins.FirstOrDefault((x) => x.Username == username);
            
            if (foundAdmin is null)
                return LoginCodes.WRONG_USERNAME;
            
            password = EncryptPassword(password);
            if(password != foundAdmin.Password)
                return LoginCodes.WRONG_PASSWORD;
                
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
