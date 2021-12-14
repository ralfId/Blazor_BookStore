using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaProducto_Cliente.Services.IService
{
    public interface IBookService
    {
        public Task<IEnumerable<BookDto>> GetBooksByAuthorAsync(string author);
        public Task<BookDto> GetBookAsync(int bookId);
    }
}
