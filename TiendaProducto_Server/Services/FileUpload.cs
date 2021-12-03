using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TiendaProducto_Server.Services.IService;

namespace TiendaProducto_Server.Services
{
    public class FileUpload : IFileUpload
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _contextAccessor;
        public FileUpload(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor contextAccessor)
        {
            _webHostEnvironment = webHostEnvironment;
            _contextAccessor = contextAccessor;
        }

        public async Task<string> UploadFileAsync(IBrowserFile file)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(file.Name);
                var fileName = Guid.NewGuid().ToString() + fileInfo.Extension;//file name
                var folderDirectory = $"{_webHostEnvironment.WebRootPath}\\images_books";
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "images_books", fileName);
                var memoryStream = new MemoryStream();
                await file.OpenReadStream().CopyToAsync(memoryStream);
                //create folder for book images if not exist
                if (!Directory.Exists(folderDirectory))
                {
                    Directory.CreateDirectory(folderDirectory);
                }

                await using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    memoryStream.WriteTo(fs);
                }


                                //get http or https                             //take the rest of the route
                var url = $"{_contextAccessor.HttpContext.Request.Scheme}://{_contextAccessor.HttpContext.Request.Host.Value}";

                //return a full route
                return $"{url}/images_books/{fileName}";


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteFile(string fileName)
        {
            try
            {
                var filePath = $"{_webHostEnvironment.WebRootPath}\\images_books\\{fileName}";

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
