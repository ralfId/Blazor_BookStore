using Models;
using Models.Api;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TiendaProducto_Cliente.Services.IService;

namespace TiendaProducto_Cliente.Services
{
    public class BookService : IBookService
    {
        private readonly HttpClient _httpClient;
        public BookService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<BookDto> GetBookAsync(int bookId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/book/{bookId}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<BookDto>(content);
                }
                else
                {
                    var content = await response.Content.ReadAsStringAsync();

                   var errorMsg = JsonConvert.DeserializeObject<ErrorModel>(content);
                    throw new Exception(errorMsg.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return null;
            }
        }

        public async Task<IEnumerable<BookDto>> GetBooksByAuthorAsync(string author)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/book?author={author}");
                var content = await response.Content.ReadAsStringAsync();
                
                return JsonConvert.DeserializeObject<IEnumerable<BookDto>>(content);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}
