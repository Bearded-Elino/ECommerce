using System.Threading.Tasks;
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
                    callback_url = "http://localhost:5231/callback"
                    
                }
            );

            var response = await _restClient.ExecuteAsync(request);
            return response.Content;
        }

        public async Task<string> VerifyTransactionAsync(string reference)
        {
            var request = new RestRequest($"transaction/verify/{reference}", Method.Get); // Correct method for GET
            request.AddHeader("Authorization", $"Bearer {_secretKey}");

            var response = await _restClient.ExecuteAsync(request);
            if (!response.IsSuccessful)
            {
                throw new Exception($"Paystack API request failed: {response.Content}");
            }

            return response.Content;
        }
    }
}