using Business.Repositories.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaProducto_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BookOrderController : Controller
    {
        private readonly IBookOrderDetailsRepository _orderDetailsRespository;
        public BookOrderController(IBookOrderDetailsRepository orderDetailsRespository)
        {
            _orderDetailsRespository = orderDetailsRespository;
        }
        public async Task<IActionResult> Create([FromBody] BookOrderDetailsDto detailsDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _orderDetailsRespository.CreateOrderAsync(detailsDto);
                return Ok(result);
            }
            else
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = "error creating order detail",
                });
            }
        }
    }
}
