namespace Project_PrzedmiotBranżowy_.Services
{
    public enum LoginCodes
    {
        OK,
        EMPTY_USERNAME,
        EMPTY_PASSWORD,
        WRONG_USERNAME,
        WRONG_PASSWORD
    }

    public interface ISecurityService
    {
        public LoginCodes Login(string? username, string? password);
    }
}
