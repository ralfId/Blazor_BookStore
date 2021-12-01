using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaProducto_Server.Helpers
{
    public static class IJSRuntimeExtensions
    {
        public static async ValueTask ToastSuccess(this IJSRuntime JsRuntime, string success_message)
        {
            await JsRuntime.InvokeVoidAsync("ToastrSuccess", success_message);
        }

        public static async ValueTask ToastWarning(this IJSRuntime JsRuntime, string warning_message)
        {
            await JsRuntime.InvokeVoidAsync("ToastrWarning", warning_message);
        }

        public static async ValueTask ToastError(this IJSRuntime JsRuntime, string error_message)
        {
            await JsRuntime.InvokeVoidAsync("ToastrError", error_message);
        }
    }
}
