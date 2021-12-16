using Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaProducto_Cliente.Services.IService
{
    public interface IAuthService
    {
        Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto requestDto);
        Task<AuthResponseDto> LoginAsync(AuthRequestDto requestDto);
        Task LogoutAsync();
    }
}
