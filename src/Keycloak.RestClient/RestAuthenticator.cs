using KeycloakRestClient.Models.Token;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serializers.NewtonsoftJson;

namespace Keycloak.RestClient
{
    public class RestAuthenticator : AuthenticatorBase
    {
        readonly string _baseUrl;
        readonly string _username;
        readonly string _password;
        readonly string _realm;

        public RestAuthenticator(string baseUrl, string username, string password, string realm) : base("")
        {
            _baseUrl = baseUrl;
            _username = username;
            _password = password;
            _realm = realm;
        }

        protected override async ValueTask<Parameter> GetAuthenticationParameter(string accessToken)
        {
            var token = string.IsNullOrEmpty(Token) ? await GetToken(_realm) : Token;
            return new HeaderParameter(KnownHeaders.Authorization, token);
        }

        async Task<string> GetToken(string realm)
        {
            var client = this.GetRestClient();

            var request = new RestRequest($"/realms/{realm}/protocol/openid-connect/token")
                .AddParameter("grant_type", "password")
                .AddParameter("username", _username)
                .AddParameter("password", _password)
                .AddParameter("client_id", "admin-cli");

            var response = await client.ExecutePostAsync<TokenResponse>(request);

            if(response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception(response.StatusCode.ToString());

            return $"{response?.Data?.TokenType} {response?.Data?.AccessToken}";
        }

        public RestSharp.RestClient GetRestClient()
        {
            var options = new RestClientOptions(_baseUrl);
            using var client = new RestSharp.RestClient(options);
            client.UseNewtonsoftJson();
            return client;
        }

    }
}
