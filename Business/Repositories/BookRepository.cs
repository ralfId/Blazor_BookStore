﻿using AutoMapper;
using Business.Repositories.IRepository;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDBContext _dbContext;
        private readonly IMapper _mapper;

        public BookRepository(AppDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<BookDto> BookExistAsync(string bookName)
        {
            try
            {
                var book = await _dbContext.Book.FirstOrDefaultAsync(x => x.Name.ToLower() == bookName.ToLower());
                return _mapper.Map<Book, BookDto>(book);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<BookDto> CreateBookAsync(BookDto bookDto)
        {
            Book book = _mapper.Map<BookDto, Book>(bookDto);
            book.Creation_date = DateTime.Now;
            book.Author = string.Empty;

            //save the new book in DB
            var newBook = await _dbContext.AddAsync(book);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<Book, BookDto>(newBook.Entity);

        }

        public async Task<int> DeleteBookAsync(int bookId)
        {
            var bookDetail = await _dbContext.Book.FindAsync(bookId);
            if (bookDetail != null)
            {
                _dbContext.Book.Remove(bookDetail);
                return await _dbContext.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
        {
            try
            {
                IEnumerable<BookDto> bookLstDto = _mapper.Map<IEnumerable<Book>, IEnumerable<BookDto>>(_dbContext.Book);
                return bookLstDto;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<BookDto> GetBookByIdAsync(int bookId)
        {
            try
            {
                var book = await _dbContext.Book.FirstOrDefaultAsync(x => x.Id == bookId);
                return _mapper.Map<Book, BookDto>(book);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<BookDto> UpdateBook(BookDto bookDto, int bookId)
        {
            try
            {
                if (bookDto.Id == bookId)
                {
                    Book bookDetails = await _dbContext.Book.FindAsync(bookId);
                    Book book = _mapper.Map<BookDto, Book>(bookDto, bookDetails);
                    book.Author = string.Empty;
                    book.Creation_date = DateTime.Now;

                    var bookUpdate = _dbContext.Update(book);

                    await _dbContext.SaveChangesAsync();

                    return _mapper.Map<Book, BookDto>(bookUpdate.Entity);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
