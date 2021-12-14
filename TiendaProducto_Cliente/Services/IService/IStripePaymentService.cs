using Models;
using Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaProducto_Cliente.Services.IService
{
    public interface IStripePaymentService
    {
        public Task<SuccessModel> CheckoutAsync(StripePaymentDto paymentDto);
    }
}
