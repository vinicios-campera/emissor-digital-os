namespace OrderService.Api
{
    public interface IApiClientBase
    {
        void SetBearerToken(string token);

        void SetBasic(string username, string password);

        void SetBaseUrl(string baseUrl);
    }
}