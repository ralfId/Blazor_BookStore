using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.IRepository
{
    public interface IBookRepository
    {
        public Task<BookDto> CreateBookAsync(BookDto bookDto);
        public Task<BookDto> GetBookByIdAsync(int bookId);
        public Task<IEnumerable<BookDto>> GetAllBooksAsync(string author = null);
        public Task<BookDto> UpdateBookAsync(BookDto bookDto, int bookId);
        public Task<BookDto> BookExistAsync(string bookName, int bookId = 0);
        public Task<int> DeleteBookAsync(int bookId);


    }
}
