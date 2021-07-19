using System.Text.Json.Serialization;

namespace Clean.Core.Dto.Firebase
{
    public class FirebaseSignUpInfoDto
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("returnSecureToken")]
        public bool ReturnSecureToken { get; set; }
    }
}
