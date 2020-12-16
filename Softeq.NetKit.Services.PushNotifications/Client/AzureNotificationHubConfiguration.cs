// Developed by Softeq Development Corporation
// http://www.softeq.com

using EnsureThat;
using Microsoft.Azure.NotificationHubs;

namespace Softeq.NetKit.Services.PushNotifications.Client
{
    public class AzureNotificationHubConfiguration
    {
        public AzureNotificationHubConfiguration(
            string connectionString,
            string hubName,
            NotificationHubSettings notificationHubSettings = null)
        {
            ConnectionString = Ensure.String.IsNotNullOrWhiteSpace(connectionString, nameof(connectionString));
            HubName = Ensure.String.IsNotNullOrWhiteSpace(hubName, nameof(hubName));
            NotificationHubSettings = notificationHubSettings ?? new NotificationHubSettings();
        }

        public string ConnectionString { get; }

        public string HubName { get; }

        public NotificationHubSettings NotificationHubSettings { get; }
    }
}
