using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        public Task<BookOrderDetailsDto> SaveOrderDetailAsync(BookOrderDetailsDto detailsDto)
        {
            throw new NotImplementedException();
        }
    }
}
