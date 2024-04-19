using DevFreela.Core.DTOs;
using DevFreela.Core.Services;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace DevFreela.Infrastructure.Payments
{
    public class PaymentService : IPaymentService
    {
        private readonly IHttpClientFactory _httpClienteFactory;
        private string _paymentsBaseUrl;


        public PaymentService(IHttpClientFactory httpClienteFactory, IConfiguration configuration)
        {
            _httpClienteFactory = httpClienteFactory;
            _paymentsBaseUrl = configuration.GetRequiredSection("Services:Payments").Value;
        }


        public async Task<bool> ProccessPayment(PaymentInfoDTO paymentInfoDTO)
        {
            var url = $"{_paymentsBaseUrl}api/payments";

            var paymentInfoJson = JsonSerializer.Serialize(paymentInfoDTO);

            var paymentInfoContent = new StringContent(
                content: paymentInfoJson,
                encoding: Encoding.UTF8,
                mediaType: "application/json"
                );

            var httpClient = _httpClienteFactory.CreateClient("Payments");

            var response = await httpClient.PostAsync(requestUri: url, content: paymentInfoContent);

            return response.IsSuccessStatusCode; 
        }
    }

}
