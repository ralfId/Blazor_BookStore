using Microsoft.AspNetCore.Components;
using Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TiendaProducto_Cliente.Services.IService;

namespace TiendaProducto_Cliente.Pages.Auth
{
    public partial class Login
    {
        [Inject]
        public IAuthService authService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        private AuthRequestDto loginReqForm { get; set; } = new AuthRequestDto();
        private bool IsBusy { get; set; }
        private bool ShowErrors { get; set; }
        private string ErrorMsg { get; set; }
        private string ReturnUrl { get; set; }
        private async Task LoginUser()
        {
            IsBusy = true;
            ShowErrors = false;

            var registerResp = await authService.LoginAsync(loginReqForm);

            if (registerResp.IsAuthenticated)
            {
                IsBusy = false;
                ShowErrors = false;


                var absoluteUri = new Uri(NavigationManager.Uri);
                var queryParam = HttpUtility.ParseQueryString(absoluteUri.Query);
                ReturnUrl = queryParam["returnUrl"];
                if (string.IsNullOrEmpty(ReturnUrl))
                {

                    NavigationManager.NavigateTo("/");
                }
                else
                {
                    NavigationManager.NavigateTo($"/{ReturnUrl}");
                }
            }
            else
            {
                IsBusy = false;
                ErrorMsg = registerResp.ErrorMessage;
                ShowErrors = true;
            }
        }
    }
}
