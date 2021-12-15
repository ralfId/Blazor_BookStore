using Models;
using Models.Api;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TiendaProducto_Cliente.Services.IService;

namespace TiendaProducto_Cliente.Services
{
    public class BookOrderDetailService : IBookOrderDetailService
    {
        private readonly HttpClient _client;
        public BookOrderDetailService(HttpClient client)
        {
            _client = client;
        }

        public Task<BookOrderDetailsDto> MarkSuccessfulPayment(BookOrderDetailsDto detailsDto)
        {
            throw new NotImplementedException();
        }

        public async Task<BookOrderDetailsDto> SaveOrderDetailAsync(BookOrderDetailsDto detailsDto)
        {
            var content = JsonConvert.SerializeObject(detailsDto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/bookorder/Create", bodyContent);

            if (response.IsSuccessStatusCode)
            {
                var tempContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<BookOrderDetailsDto>(tempContent);
            }
            else
            {
                var tempContent = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(tempContent);
                throw new Exception(errorModel.ErrorMessage);
            }
        }
    }
}
