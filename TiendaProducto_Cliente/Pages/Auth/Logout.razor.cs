using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaProducto_Cliente.Services.IService;

namespace TiendaProducto_Cliente.Pages.Auth
{
    public partial class Logout
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public IAuthService authService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await authService.LogoutAsync();
            NavigationManager.NavigateTo("/");
        }
    }
}
