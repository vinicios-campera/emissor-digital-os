namespace OrderService.Interfaces
{
    public interface IGoogleManager
    {
        void Login(Action<string, string> OnLoginComplete);

        void Logout();
    }
}