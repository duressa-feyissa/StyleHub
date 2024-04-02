using CloudinaryDotNet;

namespace Infrastructure.Configuration
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
            var cloudName = configuration.GetSection("Cloudinary:CloudName").Value;
            var apiKey = configuration.GetSection("Cloudinary:APIKey").Value;
            var apiSecret = configuration.GetSection("Cloudinary:APISecret").Value;

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
