using Newtonsoft.Json;

namespace KeycloakRestClient.Models.Token
{
    public class TokenResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }


        [JsonProperty("token_type")]
        public string TokenType { get; set; }
    }
}
