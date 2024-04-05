using Infrastructure.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Infrastructure.Configuration
{
    public class PhoneNumberOTPManager
    {
        private readonly PhoneNumberOTPSettings _phoneNumberOTPSettings;
        private readonly HttpClient _httpClient;

        public PhoneNumberOTPManager(
            IOptions<PhoneNumberOTPSettings> phoneNumberOTPSettings,
            HttpClient httpClient
        )
        {
            _phoneNumberOTPSettings = phoneNumberOTPSettings.Value;
            _httpClient = httpClient;
        }

        public async Task<string> SendOTPAsync(string phoneNumber, string code)
        {
            // var url = "YOUR_API_ENDPOINT_URL_HERE";
            // var data = new { PhoneNumber = phoneNumber, Code = code };

            // var json = JsonConvert.SerializeObject(data);

            // var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            // var response = await _httpClient.PostAsync(url, content);

            // if (response.IsSuccessStatusCode)
            //     return "OTP Sent";
            // else
            //     return "Failed to send OTP. Status code: " + response.StatusCode;
            return await Task.FromResult("OTP Sent");
        }
    }


}
