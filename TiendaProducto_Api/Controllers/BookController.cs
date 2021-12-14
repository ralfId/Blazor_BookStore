using Business.Repositories.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaProducto_Api.Controllers
{
    [Route("api/[controller]")]
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks(string author = null)
        {

            if (string.IsNullOrEmpty(author))
            {
                return BadRequest(new ErrorModel()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "parameter auhtor is required"
                });
            }

            var allBooks = await _bookRepository.GetAllBooksAsync(author);

            return Ok(allBooks);

        }

        [HttpGet("{bookId}")]
        public async Task<IActionResult> GetBookById(int? bookId)
        {
            if (bookId == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "The id of the book is invalid",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            var bookDetail = await _bookRepository.GetBookByIdAsync(bookId.Value);

            if (bookDetail == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "No results found for the specified book ID",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }

            return Ok(bookDetail);
        }
    }
}
