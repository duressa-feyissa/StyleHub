using CloudinaryDotNet;
using Infrastructure.Models;

namespace Infrastructure.Configuration
{
	public static class CloudinaryConfiguration
	{
		
		
		public static Cloudinary Configure(CloudinarySettings apiSettings)
		{
			
			var cloudinaryAccount = GetCloudinaryAccount(apiSettings);
			return new Cloudinary(cloudinaryAccount);
		}

		private static Account GetCloudinaryAccount(CloudinarySettings configuration)
		{
			var cloudName = configuration.CloudName;
			var apiKey = configuration.APIKey;
			var apiSecret = configuration.APISecret;

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
