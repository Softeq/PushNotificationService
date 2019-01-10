// Developed by Softeq Development Corporation
// http://www.softeq.com

using EnsureThat;

namespace Softeq.NetKit.Services.PushNotifications.Client
{
    public class AzureNotificationHubConfiguration
    {
        public AzureNotificationHubConfiguration(string connectionString, string hubName)
        {
            Ensure.That(connectionString, nameof(connectionString)).IsNotNullOrWhiteSpace();
            Ensure.That(hubName, nameof(hubName)).IsNotNullOrWhiteSpace();

            ConnectionString = connectionString;
            HubName = hubName;
        }

        public string ConnectionString { get; }
        public string HubName { get; }
    }
}
