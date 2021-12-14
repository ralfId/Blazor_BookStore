using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.IRepository
{
    public interface IBookOrderDetailsRepository
    {
        public Task<BookOrderDetailsDto> CreateOrderAsync(BookOrderDetailsDto details);
        public Task<BookOrderDetailsDto> MarkSuccessfulPaymentAsync(int Id);
        public Task<BookOrderDetailsDto> GetOrderDetailAsync(int bookOrderId);
        public Task<IEnumerable<BookOrderDetailsDto>> GetOrderListAsync();
        public Task<bool> UpdateOrderAsync(int bookOrderId, string status);

    }
}
