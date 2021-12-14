using Models;
using Models.Api;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TiendaProducto_Cliente.Services.IService;

namespace TiendaProducto_Cliente.Services
{
    public class StripePaymentService : IStripePaymentService
    {
        private readonly HttpClient _client;

        public StripePaymentService(HttpClient client)
        {
            _client = client;
        }

        public async Task<SuccessModel> CheckoutAsync(StripePaymentDto paymentDto)
        {
            var content = JsonConvert.SerializeObject(paymentDto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/stripepayment/Create", bodyContent);

            if (response.IsSuccessStatusCode)
            {
                var tempContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<SuccessModel>(tempContent);
            }
            else
            {
                var tempContent = await response.Content.ReadAsStringAsync();
                var errorModel =  JsonConvert.DeserializeObject<ErrorModel>(tempContent);
                throw new Exception(errorModel.ErrorMessage);
            }
        }
    }
}
