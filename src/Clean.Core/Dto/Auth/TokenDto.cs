using System.Text.Json.Serialization;

namespace Clean.Core.Dto.Auth
{
    public class TokenDto
    {
        [JsonPropertyName("token_type")]
        public string TokenType  { get; set; }

        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }
    }
}
