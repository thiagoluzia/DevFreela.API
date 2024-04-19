using DevFreela.Core.DTOs;

namespace DevFreela.Core.Services
{
    public interface IPaymentService
    {
        Task<bool> ProccessPayment(PaymentInfoDTO paymentInfoDTO);
    }
}
