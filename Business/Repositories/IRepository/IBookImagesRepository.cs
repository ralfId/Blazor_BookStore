using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.IRepository
{
    public interface IBookImagesRepository
    {
        public Task<int> AddBookImageAsync(BookImagesDto imagesDto);
        public Task<int> DeleteBookImageByIdAsync(int imageId);
        public Task<int> DeleteAllBookImagesAsync(int bookId);
        public Task<IEnumerable<BookImagesDto>> GetBookImageListAsync(int bookId);
    }
}
