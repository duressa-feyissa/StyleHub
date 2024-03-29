using CloudinaryDotNet;

namespace Infrastructure
{
    public static class CloudinaryConfiguration
    {
        public static Cloudinary Configure(IConfiguration configuration)
        {
            var cloudinaryAccount = GetCloudinaryAccount(configuration);
            return new Cloudinary(cloudinaryAccount);
        }

        private static Account GetCloudinaryAccount(IConfiguration configuration)
        {
            var cloudName = "dtghsmx0s"; //configuration["Cloudinary:CloudName"];
            var apiKey = "155646927271619"; // configuration["Cloudinary:APIKey"];
            var apiSecret = "kYyrS0ssz2NlVjQw0i17Z5ZnnfY"; // configuration["Cloudinary:APISecret"];

            if (
                string.IsNullOrEmpty(cloudName)
                || string.IsNullOrEmpty(apiKey)
                || string.IsNullOrEmpty(apiSecret)
            )
            {
                throw new ApplicationException("Cloudinary configuration is missing or invalid.");
            }

            return new Account(cloudName, apiKey, apiSecret);
        }
    }
}
