using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models;
using Models.Api;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaProducto_Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StripePaymentController : Controller
    {
        private readonly IConfiguration _configuration;
        public StripePaymentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async  Task<ActionResult> Create(StripePaymentDto paymentDto)
        {
            try
            {
                var domain = _configuration.GetValue<string>("TiendaProducto_Cliente_URL");

                var stripeOptions = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string>
                    {
                        "card",
                    },
                    LineItems = new List<SessionLineItemOptions>
                    {
                        new SessionLineItemOptions
                        {
                            PriceData = new SessionLineItemPriceDataOptions
                            {
                                UnitAmount = paymentDto.Amount*100,
                                Currency = "usd",
                                ProductData = new SessionLineItemPriceDataProductDataOptions
                                {
                                    Name = paymentDto.ProductName
                                }
                            },
                            Quantity = 1
                        }
                    },
                    Mode = "payment",
                    SuccessUrl = domain + "success-payment?session_id={{CHECKOUT_SESSION_ID}}",
                    CancelUrl = domain + paymentDto.ReturnUrl
                };

                var sessionService = new SessionService();

                Session session = await sessionService.CreateAsync(stripeOptions);

                return Ok(new SuccessModel()
                {
                    Data = session.Id
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = ex.Message
                });
            }
        }
    }
}
