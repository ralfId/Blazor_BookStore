using Blazored.LocalStorage;
using Common;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using TiendaProducto_Cliente.Helpers;

namespace TiendaProducto_Cliente.Services
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _client;
        private readonly ILocalStorageService _localStorage;


        public AuthStateProvider(HttpClient client, ILocalStorageService localStorage)
        {
            _client = client;
            _localStorage = localStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _localStorage.GetItemAsync<string>(ConstantsCommon.LS_Jwt);

            if (token == null)
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(JWTParser.ParseClaimsFromJwt(token), "jwtAuthType")));
        }
    }
}
