using RestSharp;

namespace Keycloak.RestClient
{
    public partial class KeycloakRestClient
    {
        private RestSharp.RestClient restClient;
        public KeycloakRestClient(string baseUrl, string userName, string password, string realm = "master")
        {
            restClient = new RestSharp.RestClient(baseUrl);
            restClient.Authenticator = new RestAuthenticator(baseUrl,userName, password, realm);
        }
    }
}