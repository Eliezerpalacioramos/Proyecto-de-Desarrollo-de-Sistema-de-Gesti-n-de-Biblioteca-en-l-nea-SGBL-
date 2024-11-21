namespace SGBL.Business.Services
{
    public interface IAuthService
{
    Task<bool> LoginAsync(string email, string password);
}
}

