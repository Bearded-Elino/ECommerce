using System;
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
            request.AddHeader("Content-Type", "application/json");

            var body = new
            {
                email = email,
                amount = amount * 100 // Paystack expects the amount in kobo
            };
            request.AddJsonBody(body);

            try
            {
                var response = await _restClient.ExecuteAsync(request);

                if (!response.IsSuccessful)
                {
                    // Log the response content for debugging
                    Console.WriteLine("Error Response: " + response.Content);
                    // Also log the status code
                    Console.WriteLine("Status Code: " + response.StatusCode);
                    throw new Exception($"Error initializing payment with Paystack: {response.Content}");
                }

                return response.Content;
            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.WriteLine("Exception in InitializePayment: " + ex.Message);
                throw;
            }
        }


        public async Task<string> VerifyTransactionAsync(string reference)
        {
            var request = new RestRequest($"transaction/verify/{reference}", Method.Get);
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
