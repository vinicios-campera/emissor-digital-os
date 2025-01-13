namespace OrderService.Interfaces
{
    public interface IFirebaseAuthService
    {
        Task<string> LoginWithEmailPassword(string email, string password);
        Task<string> LoginWithGoogle(string idToken);
    }
}