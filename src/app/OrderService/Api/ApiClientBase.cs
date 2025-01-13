using System.Text;

namespace OrderService.Api
{
    public class ApiClientBase : IApiClientBase
    {
        public string? BaseUrl { get; set; }
        public string? BearerToken { get; private set; }
        public Dictionary<string, string>? BasicCredentials { get; private set; }

        public void SetBaseUrl(string baseUrl) => BaseUrl = baseUrl;

        public void SetBasic(string username, string password)
        {
            BasicCredentials = new Dictionary<string, string>
            {
                { "username", username },
                { "password", password }
            };
        }

        public void SetBearerToken(string token) => BearerToken = token;

#pragma warning disable IDE0060 // Remove unused parameter

        protected Task<HttpRequestMessage> CreateHttpRequestMessageAsync(CancellationToken cancellationToken)
#pragma warning restore IDE0060 // Remove unused parameter
        {
            var req = new HttpRequestMessage();

            if (BasicCredentials != null && BasicCredentials.Any())
            {
                var basicEncoded = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{BasicCredentials["username"]}:{BasicCredentials["password"]}"));
                req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", basicEncoded);
            }

            if (!string.IsNullOrEmpty(BearerToken))
                req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", BearerToken);

            return Task.FromResult(req);
        }
    }
}