using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaProducto_Cliente.Helpers
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


        public static async ValueTask SwalSuccess(this IJSRuntime JsRuntime, string success_message)
        {
            await JsRuntime.InvokeVoidAsync("SwalSuccess", success_message);
        }

        public static async ValueTask SwalWarning(this IJSRuntime JsRuntime, string warning_message)
        {
            await JsRuntime.InvokeVoidAsync("SwalWarning", warning_message);
        }

        public static async ValueTask SwalError(this IJSRuntime JsRuntime, string error_message)
        {
            await JsRuntime.InvokeVoidAsync("SwalError", error_message);
        }
    }
}
