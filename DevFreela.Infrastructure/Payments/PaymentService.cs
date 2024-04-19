using DevFreela.Core.DTOs;
using DevFreela.Core.Services;
using System.Text;
using System.Text.Json;

namespace DevFreela.Infrastructure.Payments
{
    public class PaymentService : IPaymentService
    {
        private readonly IMessageBusService _messageBusService;
        private const string QUEUE_NAME = "Payments";


        public PaymentService(IMessageBusService messageBusService)
        {
           _messageBusService = messageBusService;
        }


        public void ProccessPayment(PaymentInfoDTO paymentInfoDTO)
        {
            // converte o objeto em json 
            var paymentsInfoJson = JsonSerializer.Serialize(paymentInfoDTO);

            // convert o json em byte aaray
            var paymentsInfopBytes = Encoding.UTF8.GetBytes(paymentsInfoJson);

            // publica a menssagem
            _messageBusService.Publish(
                queue: QUEUE_NAME,
                message: paymentsInfopBytes
                );
        }
    }

    #region CONSUMINDO UMA API MICROSSERVICE
    //using Microsoft.Extensions.Configuration;
    //using System.Net.Http;
    //public class PaymentService : IPaymentService
    //{
    //    private readonly IHttpClientFactory _httpClienteFactory;
    //    private string _paymentsBaseUrl;


    //    public PaymentService(IHttpClientFactory httpClienteFactory, IConfiguration configuration)
    //    {
    //        _httpClienteFactory = httpClienteFactory;
    //        _paymentsBaseUrl = configuration.GetRequiredSection("Services:Payments").Value;
    //    }


    //    public async Task<bool> ProccessPayment(PaymentInfoDTO paymentInfoDTO)
    //    {
    //        var url = $"{_paymentsBaseUrl}api/payments";

    //        var paymentInfoJson = JsonSerializer.Serialize(paymentInfoDTO);

    //        var paymentInfoContent = new StringContent(
    //            content: paymentInfoJson,
    //            encoding: Encoding.UTF8,
    //            mediaType: "application/json"
    //            );

    //        var httpClient = _httpClienteFactory.CreateClient("Payments");

    //        var response = await httpClient.PostAsync(requestUri: url, content: paymentInfoContent);

    //        return response.IsSuccessStatusCode;
    //    }
    //}
    #endregion
}
