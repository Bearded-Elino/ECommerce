/*using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using RestSharp;

namespace ValeShop.PaymentServices
{
    public class PaystackService
    {
        private readonly string _secretKey;
        private readonly RestClient _restClient;

        public PaystackService(IConfiguration configuration)
        {
            _secretKey = configuration["PaymentSettings:Paystack:SecretKey"];
            _restClient = new RestClient("https://api.paystack.co");
        }

        public async Task<string> InitializePayment(decimal amount, string email)
        {
            var request = new RestRequest("transaction/initialize", Method.Post);
            request.AddHeader("Authorization", $"Bearer {_secretKey}");
            request.AddHeader("Content-Type", "Application/Json");
            request.AddJsonBody(new
                {
                    email = email,
                    amount = amount * 1000,
                    callback_url = ""
                    
                }
                
            )
        }
    }
}*/