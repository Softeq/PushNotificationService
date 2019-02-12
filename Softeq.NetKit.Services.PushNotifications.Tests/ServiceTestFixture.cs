using Microsoft.Extensions.Configuration;
using Softeq.NetKit.Services.PushNotifications.Client;

namespace Softeq.NetKit.Services.PushNotifications.Tests
{
    public class ServiceTestFixture
    {
        public string ApnsDeviceToken { get; }
        public AzureNotificationHubConfiguration HubConfig { get; }

        public ServiceTestFixture()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            var configuration = builder.Build();
            HubConfig = new AzureNotificationHubConfiguration(configuration["NotificationHub:ConnectionString"],
                configuration["NotificationHub:HubName"]);

            ApnsDeviceToken = configuration["NotificationHub:ApnsDeviceToken"];
        }
    }
}
