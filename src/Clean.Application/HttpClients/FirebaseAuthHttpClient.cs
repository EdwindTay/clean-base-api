using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Clean.Application.Configurations;
using Clean.Application.Extensions;
using Clean.Application.HttpClients.Interfaces;
using Clean.Core.Dto.Auth;
using Clean.Core.Dto.Firebase;

namespace Clean.Application.HttpClients
{
    public class FirebaseAuthHttpClient : IFirebaseAuthHttpClient
    {
        private readonly HttpClient _client;
        private readonly FirebaseConfiguration _firebaseConfiguration;

        public FirebaseAuthHttpClient(HttpClient client, FirebaseConfiguration firebaseConfiguration)
        {
            _client = client;
            _firebaseConfiguration = firebaseConfiguration;

            _client.BaseAddress = new Uri(firebaseConfiguration.TokenUri);
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<TokenDto> LoginAsync(string username, string password)
        {
            FirebaseLoginInfoDto firebaseLoginInfo = new()
            {
                Email = username,
                Password = password,
                ReturnSecureToken = true
            };

            var result = await _client.PostAsJsonAsync<FirebaseLoginInfoDto, FirebaseTokenDto>(
                url: $"{_firebaseConfiguration.TokenUri}accounts:signInWithPassword?key={_firebaseConfiguration.ApiKey}",
                data: firebaseLoginInfo
            );

            TokenDto token = new()
            {
                TokenType  = "Bearer",
                AccessToken = result.IdToken
            };

            return token;
        }
    }
}
