using System;
using System.Threading.Tasks;

namespace rodriguez.Data.PayPal
{
    public interface IPayPalApiClient
    {
        Task<PayPalGetTokenResponse> GetAccessToken();
        Task<PayPalExecutePaymentResult> MakePayment();
        Task<string> ExecuteApprovedPayment(string payerId, string accessToken, string paymentId);
    }
}
