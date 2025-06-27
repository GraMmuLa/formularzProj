using Project_PrzedmiotBranzowy_Core.Helpers;

namespace Project_PrzedmiotBranzowy_Core.Services
{
    public interface ISecurityService
    {
        public LoginCodes Login(string? username, string? password);
        public LoginCodes Register(string? username, string? password, string? repeatPassword);
    }
}
