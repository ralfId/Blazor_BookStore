using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaProducto_Server.Services.IService
{
    public interface IFileUpload
    {
        Task<string> UploadFileAsync(IBrowserFile file);
        bool DeleteFile(string fileName);
    }
}
