using Models;
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

        public Task<BookDto> GetBookAsync(int bookId, string author)
        {
            throw new NotImplementedException();
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
