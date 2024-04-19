using backend.Infrastructure.Models;
using Microsoft.Extensions.Options;

namespace backend.Infrastructure.Configuration
{
    public class PhoneNumberOTPManager(
        IOptions<PhoneNumberOTPSettings> phoneNumberOTPSettings,
        HttpClient httpClient)
    {
        private readonly PhoneNumberOTPSettings _phoneNumberOTPSettings = phoneNumberOTPSettings.Value;
        private readonly HttpClient _httpClient = httpClient;

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
