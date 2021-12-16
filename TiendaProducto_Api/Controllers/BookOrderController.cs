using Business.Repositories.IRepository;
using Common;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Api;
using Stripe.Checkout;
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

        [HttpPost]
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

        [HttpPost]
        public async Task<IActionResult> MarkSuccessfulPayment([FromBody] BookOrderDetailsDto detailsDto)
        {
            var stripeSessionService = new SessionService();
            var stripeSessionDetails = stripeSessionService.Get(detailsDto.StripeSessionId);

            if (stripeSessionDetails.PaymentStatus == "paid")
            {
                var result = await _orderDetailsRespository.MarkSuccessfulPaymentAsync(detailsDto.Id);

                if (result == null)
                {
                    return BadRequest(new ErrorModel()
                    {
                        ErrorMessage = "Payment cannot be processed",
                    });
                }

                return Ok(result);
            }
            else
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = "Payment cannot be processed",
                });
            }
        }
    }
}
