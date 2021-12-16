using Microsoft.AspNetCore.Components;
using Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaProducto_Cliente.Services.IService;

namespace TiendaProducto_Cliente.Pages.Auth
{
    public partial class Register
    {
        [Inject]
        public IAuthService authService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        private RegisterRequestDto registerReqForm { get; set; } = new RegisterRequestDto();
        private bool IsBusy { get; set; }
        private bool ShowErrors { get; set; }
        private IEnumerable<string> ErrorsLst { get; set; }

        private async Task RegisterUser()
        {
            IsBusy = true;
            ShowErrors = false;

            var registerResp = await authService.RegisterAsync(registerReqForm);

            if (registerResp.IsRegistered)
            {
                IsBusy = false;
                ShowErrors = false;
                NavigationManager.NavigateTo("/login");
            }
            else
            {
                IsBusy = false;
                ErrorsLst = registerResp.Errors;
                ShowErrors = true;
            }
        }
    }
}
