using AutoMapper;
using Business.Repositories.IRepository;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories
{
    public class BookImagesRepository : IBookImagesRepository
    {
        private readonly AppDBContext _dbContext;
        private readonly IMapper _mapper;

        public BookImagesRepository(AppDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<int> AddBookImageAsync(BookImagesDto imagesDto)
        {
            try
            {
                var image = _mapper.Map<BookImagesDto, BookImages>(imagesDto);
                await _dbContext.BookImages.AddAsync(image);
                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return 0;
            }
        }

        public async Task<int> DeleteAllBookImagesAsync(int bookId)
        {
            try
            {
                var imageList = await _dbContext.BookImages.Where(x => x.BookId == bookId).ToListAsync();
                _dbContext.BookImages.RemoveRange(imageList);
                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return 0;
            }
        }

        public async Task<int> DeleteBookImageByIdAsync(int imageId)
        {
            try
            {
                var image = await _dbContext.BookImages.FindAsync(imageId);
                _dbContext.BookImages.Remove(image);
                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return 0;
            }
        }

        public async Task<IEnumerable<BookImagesDto>> GetBookImageListAsync(int bookId)
        {
            try
            {
                var imageList = await _dbContext.BookImages.Where(x => x.BookId == bookId).ToListAsync();
                
                return _mapper.Map<IEnumerable<BookImages>, IEnumerable<BookImagesDto>>(imageList);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}
