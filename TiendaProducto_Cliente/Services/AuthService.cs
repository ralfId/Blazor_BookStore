using Blazored.LocalStorage;
using Common;
using Models.Api;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TiendaProducto_Cliente.Services.IService;

namespace TiendaProducto_Cliente.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _client;
        private readonly ILocalStorageService _localStorage;

        public AuthService(HttpClient client, ILocalStorageService localStorage)
        {
            _client = client;
            _localStorage = localStorage;
        }

        public async Task<AuthResponseDto> LoginAsync(AuthRequestDto requestDto)
        {
            var content = JsonConvert.SerializeObject(requestDto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/account/LoginUser", bodyContent);
            var tempContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<AuthResponseDto>(tempContent);

            if (response.IsSuccessStatusCode)
            {
                await _localStorage.SetItemAsync(ConstantsCommon.LS_Jwt, result.Token);
                await _localStorage.SetItemAsync(ConstantsCommon.LS_UserDetails, result.UserInfo);

                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);

                return new AuthResponseDto { IsAuthenticated = true };
            }
            else
            {
                return result;
            }
        }

        public async Task LogoutAsync()
        {
            await _localStorage.RemoveItemAsync(ConstantsCommon.LS_Jwt   );
            await _localStorage.RemoveItemAsync(ConstantsCommon.LS_UserDetails);

            _client.DefaultRequestHeaders.Authorization = null;

        }

        public async Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto requestDto)
        {
            var content = JsonConvert.SerializeObject(requestDto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/account/RegisterUser", bodyContent);
            var tempContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<RegisterResponseDto>(tempContent);

            if (response.IsSuccessStatusCode)
            {
              
                return new RegisterResponseDto { IsRegistered = true };
            }
            else
            {
                return result;
            }
        }
    }
}
