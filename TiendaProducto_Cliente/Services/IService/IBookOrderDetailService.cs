using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaProducto_Cliente.Services.IService
{
    public interface IBookOrderDetailService
    {
        public Task<BookOrderDetailsDto> SaveOrderDetailAsync(BookOrderDetailsDto detailsDto);
        public Task<BookOrderDetailsDto> MarkSuccessfulPayment(BookOrderDetailsDto detailsDto);
    }
}
